using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;
public class DotweenComponent : MonoBehaviour
{
    private enum Type
    {
        Position,
        LocalPosition,
        Alpha,
        MoveY,
        MoveX,
        DelayCallback,
        LocalMoveY,
        LocalMoveX,
        Scale,
        ScaleY,
        ScaleX,
        Rotation,
    }
    private enum Loop
    {
        Once,
        Restart,
        Yoyo
    }


    [System.Serializable]
    private class DoTweenInfo
    {
        public Type TweenType;
        public float Value;
        public Vector3 To;
        public float Duration = 0;
        public float Delay = 0;
        public Ease EaseType;
        public bool customGraph;
        public AnimationCurve customCurve;
        public UnityEvent Callback;
    }

    [SerializeField]
    private List<DoTweenInfo> Tweens = new List<DoTweenInfo>();

    [SerializeField]
    private Loop LoopType = Loop.Once;
    [SerializeField]
    private bool AutoPlay = true;

    [SerializeField]
    private bool OnEnablePlay = false;

    private Sequence seq;

    private void Awake()
    {
        seq = DOTween.Sequence();

        for (int i = 0; i < Tweens.Count; i++)
        {
            switch (Tweens[i].TweenType)
            {
                case Type.Position:
                    {
                        if (!Tweens[i].customGraph)
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOMove(Tweens[i].To, Tweens[i].Duration).SetEase(Tweens[i].EaseType));
                        else
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOMove(Tweens[i].To, Tweens[i].Duration).SetEase(Tweens[i].customCurve));
                    }
                    break;

                case Type.LocalPosition:
                    {
                        if (!Tweens[i].customGraph)
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOLocalMove(Tweens[i].To, Tweens[i].Duration).SetEase(Tweens[i].EaseType));
                        else
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOLocalMove(Tweens[i].To, Tweens[i].Duration).SetEase(Tweens[i].customCurve));
                    }
                    break;
                case Type.MoveY:
                    {
                        if (!Tweens[i].customGraph)
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOMoveY(Tweens[i].Value, Tweens[i].Duration).SetEase(Tweens[i].EaseType));
                        else
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOMoveY(Tweens[i].Value, Tweens[i].Duration).SetEase(Tweens[i].customCurve));
                    }
                    break;
                case Type.MoveX:
                    {
                        if (!Tweens[i].customGraph)
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOMoveX(Tweens[i].Value, Tweens[i].Duration).SetEase(Tweens[i].EaseType));
                        else
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOMoveX(Tweens[i].Value, Tweens[i].Duration).SetEase(Tweens[i].customCurve));
                    }
                    break;
                case Type.LocalMoveY:
                    {
                        if (!Tweens[i].customGraph)
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOLocalMoveY(Tweens[i].Value, Tweens[i].Duration).SetEase(Tweens[i].EaseType));
                        else
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOLocalMoveY(Tweens[i].Value, Tweens[i].Duration).SetEase(Tweens[i].customCurve));
                    }
                    break;
                case Type.LocalMoveX:
                    {
                        if (!Tweens[i].customGraph)
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOLocalMoveX(Tweens[i].Value, Tweens[i].Duration).SetEase(Tweens[i].EaseType));
                        else
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOLocalMoveX(Tweens[i].Value, Tweens[i].Duration).SetEase(Tweens[i].customCurve));
                    }
                    break;
                case Type.Alpha:
                    {
                        TweenParams tweenParams = new TweenParams();
                        if (Tweens[i].customGraph) { tweenParams.SetEase(Tweens[i].customCurve); }
                        else { tweenParams.SetEase(Tweens[i].EaseType); }

                        Image img = GetComponent<Image>();
                        if (img != null)
                        {
                            seq.AppendInterval(Tweens[i].Delay).Append(img.DOFade(Tweens[i].Value, Tweens[i].Duration).SetAs(tweenParams));
                            break;
                        }

                        SpriteRenderer spRen = GetComponent<SpriteRenderer>();
                        if (spRen != null)
                        {
                            seq.AppendInterval(Tweens[i].Delay).Append(spRen.DOFade(Tweens[i].Value, Tweens[i].Duration).SetAs(tweenParams));
                            break;
                        }

                        Text text = GetComponent<Text>();
                        if (text != null)
                        {
                            seq.AppendInterval(Tweens[i].Delay).Append(text.DOFade(Tweens[i].Value, Tweens[i].Duration).SetAs(tweenParams));
                            break;
                        }

                        CanvasGroup canvas = GetComponent<CanvasGroup>();
                        if (canvas != null)
                        {
                            seq.AppendInterval(Tweens[i].Delay).Append(canvas.DOFade(Tweens[i].Value, Tweens[i].Duration).SetAs(tweenParams));
                            break;
                        }

                    }
                    break;
                case Type.Scale:
                    {
                        if (!Tweens[i].customGraph)
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOScale(Tweens[i].To, Tweens[i].Duration).SetEase(Tweens[i].EaseType));
                        else
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOScale(Tweens[i].To, Tweens[i].Duration).SetEase(Tweens[i].customCurve));
                    }
                    break;
                case Type.ScaleX:
                    {
                        if (!Tweens[i].customGraph)
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOScaleX(Tweens[i].Value, Tweens[i].Duration).SetEase(Tweens[i].EaseType));
                        else
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOScaleX(Tweens[i].Value, Tweens[i].Duration).SetEase(Tweens[i].customCurve));
                    }
                    break;
                case Type.ScaleY:
                    {
                        if (!Tweens[i].customGraph)
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOScaleY(Tweens[i].Value, Tweens[i].Duration).SetEase(Tweens[i].EaseType));
                        else
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DOScaleY(Tweens[i].Value, Tweens[i].Duration).SetEase(Tweens[i].customCurve));
                    }
                    break;
                case Type.Rotation:
                    {
                        if (!Tweens[i].customGraph)
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DORotate(Tweens[i].To, Tweens[i].Duration).SetEase(Tweens[i].EaseType));
                        else
                            seq.AppendInterval(Tweens[i].Delay).Append(this.transform.DORotate(Tweens[i].To, Tweens[i].Duration).SetEase(Tweens[i].customCurve));
                    }
                    break;
                case Type.DelayCallback:
                    {
                        var idx = i;
                        seq.AppendInterval(Tweens[i].Delay).AppendCallback(() => { Tweens[idx].Callback?.Invoke(); }).AppendInterval(Tweens[i].Duration);
                    }
                    break;
            }
        }

        if (Tweens.Count > 0)
        {
            if (LoopType == Loop.Restart)
                seq.SetLoops(-1, DG.Tweening.LoopType.Restart);
            else if (LoopType == Loop.Yoyo)
                seq.SetLoops(-1, DG.Tweening.LoopType.Yoyo);
            else if (LoopType == Loop.Once)


                if (AutoPlay)
                    seq.Play();
                else
                    seq.Pause();
        }

        seq.SetAutoKill(false);
    }

    public void SetId(string id)
    {
        seq.SetId(id);
    }

    public void OnEnable()
    {
        if (seq != null && LoopType == Loop.Once && AutoPlay)
        {
            seq.Restart();
        }
    }

    public void Play()
    {
        if (seq != null)
        {
            seq.Play();
        }
    }

    public void Restart()
    {
        if (seq != null)
        {
            seq.Restart();
        }
    }

    public void Pause()
    {
        if (seq != null)
        {
            seq.Pause();
        }
    }

    private void OnDestroy()
    {
        seq.Kill();
    }





    // 두트윈 에디터 전용 Preview
    #region "Dotween Preview Editor"

    public Sequence GetSequence() { return seq; }
    public void InitSequence()
    {
        if (seq != null) { seq.Kill(); }
        Awake();
    }
    #endregion


}
