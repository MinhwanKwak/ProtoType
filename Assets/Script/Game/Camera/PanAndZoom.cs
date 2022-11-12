    using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

/// <summary> A modular and easily customisable Unity MonoBehaviour for handling swipe and pinch motions on mobile. </summary>
public class PanAndZoom : MonoBehaviour {

    /// <summary> Called as soon as the player touches the screen. The argument is the screen position. </summary>
    public event Action<Vector2> onStartTouch;
    /// <summary> Called as soon as the player stops touching the screen. The argument is the screen position. </summary>
    public event Action<Vector2> onEndTouch;
    /// <summary> Called if the player completed a quick tap motion. The argument is the screen position. </summary>
    //public event Action<Vector2> onTap;
    /// <summary> Called if the player swiped the screen. The argument is the screen movement delta. </summary>
    public event Action<Vector2> onSwipe;
    /// <summary> Called if the player pinched the screen. The arguments are the distance between the fingers before and after. </summary>
    public event Action<float, float> onPinch;

    [Header("Tap")]
    [Tooltip("The maximum movement for a touch motion to be treated as a tap")]
    public float maxDistanceForTap = 40;
    [Tooltip("The maximum duration for a touch motion to be treated as a tap")]
    public float maxDurationForTap = 0.4f;

    [Header("Desktop debug")]
    [Tooltip("Use the mouse on desktop?")]
    public bool useMouse = true;
    [Tooltip("The simulated pinch speed using the scroll wheel")]
    public float mouseScrollSpeed = 2;

    [Header("Camera control")]
    [Tooltip("Does the script control camera movement?")]
    public bool controlCamera = true;
    [Tooltip("The controlled camera, ignored of controlCamera=false")]
    public Camera cam;
    public bool IsZoomOutOver {get { return zoomOutSize < cam.orthographicSize;}}

    [Header("UI")]
    [Tooltip("Are touch motions listened to if they are over UI elements?")]
    public bool ignoreUI = false;

    [Header("Bounds")]
    [Tooltip("Is the camera bound to an area?")]
    public bool useBounds;


    public float boundMinX = -150;
    public float boundMaxX = 150;
    public float boundMinY = -150;
    public float boundMaxY = 150;

    [Header("Etc")]
    public float focusSize = 15f;
    public float focusMoveDuration = 2f;
    public float zoomOutSizeDefault = 20f;

    bool follow = false;
    Transform followTrans;
    bool focusing = false;    
    bool moving = false;
    float dragSpeed = 5f;
    Vector3 movingTarget = Vector3.zero;
    Vector3 distanceOrigin = Vector3.zero;
    Vector3 focusTargetPos;
    Vector3 focusOriginPos;
    float focusOriginCameraSize = 0f;
    float focusDeltaTime = 0f;
    Vector2 touch0StartPosition;
    Vector2 touch0LastPosition;
    float touch0StartTime;    
    [HideInInspector]
    public float zoomOutSize= 0f;    

    bool cameraControlEnabled = true;

    public Action OnFoucusEnd = null;

    bool canUseMouse;

    /// <summary> Has the player at least one finger on the screen? </summary>
    public bool isTouching { get; private set; }

    /// <summary> The point of contact if it exists in Screen space. </summary>
    public Vector2 touchPosition { get { return touch0LastPosition; } }
    private float timeRealDragStop;
    private Vector3 cameraScrollVelocity;
    private AnimationCurve autoScrollDampCurve = new AnimationCurve(new Keyframe(0, 1, 0, 0), new Keyframe(0.7f, 0.9f, -0.5f, -0.5f), new Keyframe(1, 0.01f, -0.85f, -0.85f));
    private Vector3 camVelocity = Vector3.zero;
    private Vector3 posLastFrame = Vector3.zero;
    private bool multiTouch = false;

    void Start() {
        var aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
        var isTablet = (BanpoFri.Utility.DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f);

        if(isTablet)
        {
            boundMinX = -5.0f;
            boundMaxX = 5.0f; 
        }
        else
        {
            boundMinX = -1f;
            boundMaxX = 7f;
        }

        canUseMouse = Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer && Input.mousePresent;
        cam.orthographicSize = Mathf.Min(cam.orthographicSize, ((boundMaxY - boundMinY) / 2) - 0.001f);
        zoomOutSize = cam.orthographicSize = Mathf.Min(cam.orthographicSize, (Screen.height * (boundMaxX - boundMinX) / (2 * Screen.width)) - 0.001f);
    }

    void LateUpdate() {
        CameraInBounds();
        camVelocity = (cam.transform.position - posLastFrame) / Time.deltaTime;
        posLastFrame = cam.transform.position;
    }

	private void Update()
	{
        if (useMouse && canUseMouse)
        {
            UpdateWithMouse();
        }
        else
        {
            UpdateWithTouch();
        }

        if (cameraScrollVelocity.sqrMagnitude > float.Epsilon)
        {
            float timeSinceDragStop = Time.realtimeSinceStartup - timeRealDragStop;
            float dampFactor = Mathf.Clamp01(timeSinceDragStop * 2f);
            float camScrollVel = cameraScrollVelocity.magnitude;
            float camScrollVelRelative = camScrollVel / 60;
            Vector3 camVelDamp = dampFactor * cameraScrollVelocity.normalized * 300f * Time.deltaTime;
            camVelDamp *= EvaluateAutoScrollDampCurve(Mathf.Clamp01(1.0f - camScrollVelRelative));
            if (camVelDamp.sqrMagnitude >= cameraScrollVelocity.sqrMagnitude)
            {
                cameraScrollVelocity = Vector3.zero;
            }
            else
            {
                cameraScrollVelocity -= camVelDamp;
            }
        }
        else
            moving = false;
    }

    private float EvaluateAutoScrollDampCurve(float t)
    {
        if (autoScrollDampCurve == null || autoScrollDampCurve.length == 0)
        {
            return (1);
        }
        return autoScrollDampCurve.Evaluate(t);
    }
    
    void UpdateWithMouse() {
        if (Input.GetMouseButtonDown(0)) {
            if (ignoreUI || (!focusing && !IsPointerOverUIObject(Input.mousePosition))) {
                touch0StartPosition = Input.mousePosition;
                touch0StartTime = Time.time;
                touch0LastPosition = touch0StartPosition;
                moving = false;
                isTouching = true;
                distanceOrigin = cam.transform.position;

                if (onStartTouch != null) onStartTouch(Input.mousePosition);
            }
        }

        if (Input.GetMouseButton(0) && isTouching) {
            Vector2 move = (Vector2)Input.mousePosition - touch0LastPosition;
            touch0LastPosition = Input.mousePosition;
            

            if (move != Vector2.zero) {
                OnSwipe(move);
            }
        }

        if (Input.GetMouseButtonUp(0) && isTouching) {
            if (Time.time - touch0StartTime <= maxDurationForTap
               && Vector2.Distance(Input.mousePosition, touch0StartPosition) <= maxDistanceForTap) {
                OnClick(Input.mousePosition);
            }

            if (onEndTouch != null) onEndTouch(Input.mousePosition);
            isTouching = false;
            cameraControlEnabled = true;
            
            CheckMoveTarget((Vector2)Input.mousePosition);
        }

        if (Input.mouseScrollDelta.y != 0) {
			//if(!IsPointerOverUIObject())
			//	OnPinch(Input.mousePosition, 1, Input.mouseScrollDelta.y < 0 ? (1 / mouseScrollSpeed) : mouseScrollSpeed, Vector2.right);
        }
    }

    void CheckMoveTarget(Vector2 input, bool touch = false)
    {
        timeRealDragStop = Time.realtimeSinceStartup;
        cameraScrollVelocity = -camVelocity * 0.5f;
        moving = true;
        //var move = input - touch0LastPosition;
        //var camMove = cam.ScreenToWorldPoint(move);
        //var camOrigin = cam.ScreenToWorldPoint(Vector2.zero);
        //timeRealDragStop = Time.realtimeSinceStartup;
        //cameraScrollVelocity = -camVelocity * 0.5f;

        //if ((camMove - camOrigin).sqrMagnitude < 0.1f)
        //    return;

        //Vector3 dir;
        //if(touch)
        //{
        //    dir = (camMove - camOrigin).normalized * 15f;
        //    dragSpeed = 7.5f;
        //}
        //else
        //{
        //    dir = (camMove - camOrigin).normalized * 6f;
        //}

        //moving = true;
        //cameraScrollVelocity = dir;
        //movingTarget = cam.transform.position - dir;

        //Vector2 margin = cam.ScreenToWorldPoint((Vector2.up * Screen.height / 2) + (Vector2.right * Screen.width / 2)) - cam.ScreenToWorldPoint(Vector2.zero);

        //float marginX = margin.x;
        //float marginY = margin.y;

        //float camMaxX = boundMaxX - marginX;
        //float camMaxY = boundMaxY - marginY;
        //float camMinX = boundMinX + marginX;
        //float camMinY = boundMinY + marginY;       

        //if(moving)
        //{
        //    if(camMinX > movingTarget.x )
        //        movingTarget = new Vector3(camMinX, movingTarget.y, movingTarget.z);
        //    if(camMaxX < movingTarget.x  )
        //        movingTarget = new Vector3(camMaxX, movingTarget.y, movingTarget.z);
        //    if(camMinY > movingTarget.y )
        //        movingTarget = new Vector3(movingTarget.x, camMinY, movingTarget.z);
        //    if(camMaxY < movingTarget.y )
        //        movingTarget = new Vector3(movingTarget.x, camMaxY, movingTarget.z);                
        //}
    }

    void UpdateWithTouch() {
        int touchCount = Input.touches.Length;

		if(touchCount > 1)
		{
			for (var i = 0; i < touchCount; ++i)
			{
				Touch touch = Input.touches[i];

				if (touch.phase == TouchPhase.Ended)
				{
					if (!IsPointerOverUIObject(touch.position))
					{
						IsClickInGameObject(touch.position);
					}
				}
			}
            multiTouch = true;
		}
        else if (touchCount == 1)
        {
			Touch touch = Input.touches[0];

			switch (touch.phase)
			{
				case TouchPhase.Began:
					{
						if (ignoreUI || (!focusing && !IsPointerOverUIObject(touch.position)))
						{
							touch0StartPosition = touch.position;
							touch0StartTime = Time.time;
							touch0LastPosition = touch0StartPosition;
							moving = false;
							isTouching = true;

							//if (onStartTouch != null) onStartTouch(touch0StartPosition);
						}

						break;
					}
				case TouchPhase.Moved:
					{
						touch0LastPosition = touch.position;

						if (touch.deltaPosition != Vector2.zero && isTouching)
						{
							OnSwipe(touch.deltaPosition);
						}
						break;
					}
				case TouchPhase.Ended:
					{
                        if(multiTouch)
                        {
                            if (!IsPointerOverUIObject(touch.position))
                            {
                                IsClickInGameObject(touch.position);
                            }
                        }
                        else
						if (Time.time - touch0StartTime <= maxDurationForTap
							&& Vector2.Distance(touch.position, touch0StartPosition) <= maxDistanceForTap
							&& isTouching)
						{
							OnClick(touch.position);
						}

						if (onEndTouch != null) onEndTouch(touch.position);
						isTouching = false;
						cameraControlEnabled = true;
                        multiTouch = false;

						CheckMoveTarget(touch.position, true);
						break;
					}
				case TouchPhase.Stationary:
				case TouchPhase.Canceled:
					break;
			}            
		}
    }

    void OnClick(Vector2 position) {
        // if (onTap != null && (ignoreUI || (!focusing && !IsPointerOverUIObject() && !IsClickInGameObject() ))) {
        //     onTap(position);
        // }
        if(ignoreUI || (!focusing && !IsPointerOverUIObject(position) && !IsClickInGameObject(position) ))
        {
            
        }
    }
    void OnSwipe(Vector2 deltaPosition) {
        moving = false;
        if (onSwipe != null) {
            onSwipe(deltaPosition);
        }

        if (controlCamera && cameraControlEnabled) {
            if (cam == null) cam = Camera.main;

            cam.transform.position -= (cam.ScreenToWorldPoint(deltaPosition) - cam.ScreenToWorldPoint(Vector2.zero));
        }
    }
    void OnPinch(Vector2 center, float oldDistance, float newDistance, Vector2 touchDelta) {
        moving = false;
        if (onPinch != null) {
            onPinch(oldDistance, newDistance);
        }

        if (controlCamera && cameraControlEnabled) {
            if (cam == null) cam = Camera.main;

            if (cam.orthographic) {
                var currentPinchPosition = cam.ScreenToWorldPoint(center);

                cam.orthographicSize = Mathf.Max(5f, cam.orthographicSize * oldDistance / newDistance);

                var newPinchPosition = cam.ScreenToWorldPoint(center);

                cam.transform.position -= newPinchPosition - currentPinchPosition;
            } else {
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView * oldDistance / newDistance, 0.1f, 179.9f);
            }
        }
    }

    /// <summary> Checks if the the current input is over canvas UI </summary>
    public bool IsPointerOverUIObject(Vector2 touchPosition) {
        if (EventSystem.current == null) return false;
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(touchPosition.x, touchPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public Vector2 RandomPointInBounds() 
    {
		var paddingX = (Mathf.Abs(boundMinX) + Mathf.Abs(boundMaxX)) * 0.1f;
		var paddingY = (Mathf.Abs(boundMinY) + Mathf.Abs(boundMaxY)) * 0.2f;

		return new Vector2(
            UnityEngine.Random.Range(boundMinX + paddingX, boundMaxX - paddingX),
            UnityEngine.Random.Range(boundMinY + paddingY, boundMaxY - paddingY)
        );
    }

	//private bool IsClickDust()
	//{
	//	var point = cam.ScreenToWorldPoint(Input.mousePosition);
	//	var ray = new Ray2D(point, Vector2.zero);
	//	RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

	//	if (hit.collider != null)
	//	{
	//		var dust = hit.collider.gameObject.GetComponent<Dust>();
	//		if (dust != null)
	//		{
	//			dust.Pressd();
	//			return true;
	//		}
	//	}
	//	return false;
	//}

    public bool IsClickInGameObject(Vector2 touchPosition)
    {
        var point = cam.ScreenToWorldPoint(touchPosition);
        var ray = new Ray2D(point, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        
        if(hit.collider != null)
        {
            var cc = hit.collider.gameObject.GetComponent<ClickCallback>();
            if(cc != null)
            {
                cc.Click(hit.collider.gameObject.tag);
            }
        }        
        return false;
    }

    public void FocusPositionImmediately(Vector3 worldPos, float _focusSize = 15f)
    {
        cam.orthographicSize = _focusSize;
        cam.transform.position = worldPos;
    }
    
    public void FocusPosition(Vector3 worldPos, float _focusSize = 15f)
    {
        moving = false;
        focusing = true;
        follow = false;
        focusTargetPos = new Vector3(worldPos.x, worldPos.y, cam.transform.position.z);
        focusOriginPos = cam.transform.position;
        focusDeltaTime = 0f;
        focusSize = _focusSize;
        focusOriginCameraSize = cam.orthographicSize;
    }

    public void FollowCameraPos(Transform worldTrans, float _focusSize = 10f)
    {
        moving = false;
        focusing = false;
        follow = true;
        followTrans = worldTrans;
        focusSize = _focusSize;
        focusDeltaTime = 0f;
        focusOriginCameraSize = cam.orthographicSize;
    }

    public void EndFollow()
    {
        follow = false;
    }

    public void FocusOut()
    {
        moving = false;
        focusing = true;
        follow = false;
        focusTargetPos = cam.transform.position;
        focusOriginPos = cam.transform.position;
        focusDeltaTime = 0f;
        focusSize = zoomOutSize;
        focusOriginCameraSize = cam.orthographicSize;        
    }

    /// <summary> Cancels camera movement for the current motion. Resets to use camera at the end of the touch motion.</summary>
    public void CancelCamera() {
        cameraControlEnabled = false;
    }

    void CameraInBounds() {
        if(follow)
        {
            //cam.orthographicSize = Mathf.Lerp(focusOriginCameraSize, focusSize, Easing.Quartic.Out(focusDeltaTime / focusMoveDuration));
            cam.transform.position = new Vector3(cam.transform.position.x, followTrans.position.y, cam.transform.position.z);
            //if(focusDeltaTime < focusMoveDuration)
            //    focusDeltaTime += Time.deltaTime;
            //else
            //    cam.orthographicSize = focusSize;
            return;
        }

        if(focusing)
        {
            if(focusDeltaTime >= focusMoveDuration)
            {
                focusing = false;
                TpLog.Log("focusing false");
                cam.orthographicSize = focusSize;
                if(OnFoucusEnd != null)
                {
                    OnFoucusEnd.Invoke();
                    OnFoucusEnd = null;                    
                }
                return;
            }
            moving = false;
            var result = Vector3.Lerp(focusOriginPos, focusTargetPos, Easing.Quartic.Out(focusDeltaTime / focusMoveDuration));
            cam.transform.position = new Vector3( result.x, result.y, cam.transform.position.z);
            cam.orthographicSize = Mathf.Lerp(focusOriginCameraSize, focusSize, Easing.Quartic.Out(focusDeltaTime / focusMoveDuration));
            focusDeltaTime += Time.deltaTime;
        }
        else
        if(moving)
        {
            Vector2 autoScrollVector = -cameraScrollVelocity * Time.deltaTime;
            cam.transform.position = new Vector3(cam.transform.position.x + autoScrollVector.x,
                cam.transform.position.y + autoScrollVector.y,
                cam.transform.position.z);
                
            if (onSwipe != null) {
                onSwipe(autoScrollVector);
            }
        }

        if(controlCamera && useBounds && cam != null && cam.orthographic) {
            cam.orthographicSize = Mathf.Min(cam.orthographicSize, ((boundMaxY - boundMinY) / 2) - 0.001f);
            cam.orthographicSize = Mathf.Min(cam.orthographicSize, (Screen.height * (boundMaxX - boundMinX) / (2 * Screen.width)) - 0.001f);

            Vector2 margin = cam.ScreenToWorldPoint((Vector2.up * Screen.height / 2) + (Vector2.right * Screen.width / 2)) - cam.ScreenToWorldPoint(Vector2.zero);

            float marginX = margin.x;
            float marginY = margin.y;

            float camMaxX = boundMaxX - marginX;
            float camMaxY = boundMaxY - marginY;
            float camMinX = boundMinX + marginX;
            float camMinY = boundMinY + marginY;

            bool over = false;
            Vector3 target = cam.transform.position;
            if (!isTouching)
			{                
                if (cam.transform.position.x < camMinX)
                {
                    over = true;
                    target.x = camMinX;
                }
                else if (cam.transform.position.x > camMaxX)
                {
                    over = true;
                    target.x = camMaxX;
                }
                if (cam.transform.position.y < camMinY)
                {
                    over = true;
                    target.y = camMinY;
                }
                else if (cam.transform.position.y > camMaxY)
                {
                    over = true;
                    target.y = camMaxY;
                }
                if (over)
                {
                    cam.transform.position = target;//Vector3.Lerp(cam.transform.position, target,  Time.deltaTime * 5f);
                    //cam.transform.position = new Vector3(result.x, result.y, cam.transform.position.z);

                    //FocusPosition(target, cam.orthographicSize);
                }
            }
            else
			{
                //camMaxX *= 1.4f;
                //camMaxY *= 1.4f;
                //camMinX *= 1.4f;
                //camMinY *= 1.4f;

                if (cam.transform.position.x < camMinX)
                {
                    over = true;
                    target.x = camMinX;
                }
                else if (cam.transform.position.x > camMaxX)
                {
                    over = true;
                    target.x = camMaxX;
                }
                if (cam.transform.position.y < camMinY)
                {
                    over = true;
                    target.y = camMinY;
                }
                else if (cam.transform.position.y > camMaxY)
                {
                    over = true;
                    target.y = camMaxY;
                }

                if (over)
                {
                    cam.transform.position = target;
                }
            }

            //float camX = Mathf.Clamp(cam.transform.position.x, camMinX, camMaxX);
            //float camY = Mathf.Clamp(cam.transform.position.y, camMinY, camMaxY);
           
        }
    }

    //public Texture2D TakeScreenShot(bool _blur = true)
    //{
    //    if(blur != null && _blur)
    //        blur.enabled = true;

    //    //yield return new WaitForEndOfFrame();
    //    cam.targetTexture = RenderTexture.GetTemporary( _blur ? Screen.width/4 : Screen.width, _blur ? Screen.height/4 : Screen.height, 24);
    //    cam.Render();

    //    // Activate the temporary render texture
    //    RenderTexture previouslyActiveRenderTexture = RenderTexture.active;
    //    RenderTexture.active = cam.targetTexture;

    //    // Extract the image into a new texture without mipmaps
    //    Texture2D texture = new Texture2D(cam.targetTexture.width, cam.targetTexture.height, TextureFormat.RGB24, false);
    //    texture.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
    //    texture.Apply(false);

    //    // Reactivate the previously active render texture
    //    RenderTexture.active = previouslyActiveRenderTexture;

    //    if (blur != null && _blur)
    //        blur.enabled = false;
    //    // Clean up after ourselves
    //    cam.targetTexture = null;
    //    RenderTexture.ReleaseTemporary(cam.targetTexture);
    //    return texture;
    //}
}