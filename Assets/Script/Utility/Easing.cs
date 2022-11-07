using UnityEngine;

/* 
 * Most functions taken from Tween.js - Licensed under the MIT license
 * at https://github.com/sole/tween.js
 * Quadratic.Bezier by @fonserbc - Licensed under WTFPL license
 */
public delegate float EasingFunction(float k);

public class Easing
{
	public static float Linear (float k) {
		return k;
	}
	
	public class Quadratic
	{
		public static float In (float k) {
			return k*k;
		}
		
		public static float Out (float k) {
			return k*(2f - k);
		}
		
		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return 0.5f*k*k;
			return -0.5f*((k -= 1f)*(k - 2f) - 1f);
		}
		
		public static float OutIn (float k) {
			return k<.5f ? Quadratic.Out(2f * k) * .5f : Quadratic.In((2f * (k - .5f))) * .5f + 0.5f;
		}
		
		public static float Test (float k) {
			return (k*k*k - 2f*k*k + k)*3f + (-2*k*k*k + 3*k*k) + 3f *(k*k*k-k*k)  ;
		}

		/* 
		 * Quadratic.Bezier(k,0) behaves like Quadratic.In(k)
		 * Quadratic.Bezier(k,1) behaves like Quadratic.Out(k)
		 *
		 * If you want to learn more check Alan Wolfe's post about it http://www.demofox.org/bezquad1d.html
		 */
		public static float Bezier (float k, float c) {
			return c*2*k*(1 - k) + k*k;
		}
	};
	
	public class Cubic
	{		
		public static float In (float k) {
			return k*k*k;
		}
		
		public static float Out (float k) {
			return 1f + ((k -= 1f)*k*k);
		}
		
		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return 0.5f*k*k*k;
			return 0.5f*((k -= 2f)*k*k + 2f);
		}
		public static float OutIn (float k) {
			// if ((k *= 2f) < 1f) return 0.5f * (1f - ((k -= 1f)*k*k*k));
			// return 0.5f*k*k*k*k;

			return k<.5f ? Cubic.Out(2f * k) * .5f : Cubic.In((2f * (k - .5f))) * .5f + 0.5f;
		}
	};
	
	public class Quartic
	{		
		public static float In (float k) {
			return k*k*k*k;
		}
		
		public static float Out (float k) {
			return 1f - ((k -= 1f)*k*k*k);
		}
		
		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return 0.5f*k*k*k*k;
			return -0.5f*((k -= 2f)*k*k*k - 2f);
		}
		public static float OutIn (float k) {
			// if ((k *= 2f) < 1f) return 0.5f * (1f - ((k -= 1f)*k*k*k));
			// return 0.5f*k*k*k*k;

			return k<.5f ? Quartic.Out(2f * k) * .5f : Quartic.In((2f * (k - .5f))) * .5f + 0.5f;
		}
	};
	
	public class Quintic
	{		
		public static float In (float k) {
			return k*k*k*k*k;
		}
		
		public static float Out (float k) {
			return 1f + ((k -= 1f)*k*k*k*k);
		}
		
		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return 0.5f*k*k*k*k*k;
			return 0.5f*((k -= 2f)*k*k*k*k + 2f);
		}

		public static float OutIn (float k) {
			// if ((k *= 2f) < 1f) return 0.5f * (1f - ((k -= 1f)*k*k*k));
			// return 0.5f*k*k*k*k;

			return k<.5f ? Quintic.Out(2f * k) * .5f : Quintic.In((2f * (k - .5f))) * .5f + 0.5f;
		}
	};
	
	public class Sinusoidal
	{		
		public static float In (float k) {
			return 1f - Mathf.Cos(k*Mathf.PI/2f);
		}
		
		public static float Out (float k) {
			return Mathf.Sin(k*Mathf.PI/2f);
		}
		
		public static float InOut (float k) {
			return 0.5f*(1f - Mathf.Cos(Mathf.PI*k));
		}

		public static float OutIn (float k) {
			return k<.5f ? Sinusoidal.Out(2f * k) * .5f : Sinusoidal.In((2f * (k - .5f))) * .5f + 0.5f;
		}
	};
	
	public class Exponential
	{		
		public static float In (float k) {
			return k == 0f? 0f : Mathf.Pow(1024f, k - 1f);
		}
		
		public static float Out (float k) {
			return k == 1f? 1f : 1f - Mathf.Pow(2f, -10f*k);
		}
		
		public static float InOut (float k) {
			if (k == 0f) return 0f;
			if (k == 1f) return 1f;
			if ((k *= 2f) < 1f) return 0.5f*Mathf.Pow(1024f, k - 1f);
			return 0.5f*(-Mathf.Pow(2f, -10f*(k - 1f)) + 2f);
		}

		public static float OutIn (float k) {
			return k<.5f ? Exponential.Out(2f * k) * .5f : Exponential.In((2f * (k - .5f))) * .5f + 0.5f;
		}
	};
	
	public class Circular
	{		
		public static float In (float k) {
			return 1f - Mathf.Sqrt(1f - k*k);
		}
		
		public static float Out (float k) {
			return Mathf.Sqrt(1f - ((k -= 1f)*k));
		}
		
		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return -0.5f*(Mathf.Sqrt(1f - k*k) - 1);
			return 0.5f*(Mathf.Sqrt(1f - (k -= 2f)*k) + 1f);
		}	

		public static float OutIn (float k) {
			return k<.5f ? Circular.Out(2f * k) * .5f : Circular.In((2f * (k - .5f))) * .5f + 0.5f;
		}	
	};
	
	public class Elastic
	{
		public static float In (float k) {
			if (k == 0) return 0;
			if (k == 1) return 1;
			return -Mathf.Pow( 2f, 10f*(k -= 1f))*Mathf.Sin((k - 0.1f)*(2f*Mathf.PI)/0.4f);
		}
		
		public static float Out (float k) {
			if (k == 0) return 0;
			if (k == 1) return 1;
			return Mathf.Pow(2f, -10f*k)*Mathf.Sin((k - 0.1f)*(2f*Mathf.PI)/0.4f) + 1f;
		}
		
		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return -0.5f*Mathf.Pow(2f, 10f*(k -= 1f))*Mathf.Sin((k - 0.1f)*(2f*Mathf.PI)/0.4f);
			return Mathf.Pow(2f, -10f*(k -= 1f))*Mathf.Sin((k - 0.1f)*(2f*Mathf.PI)/0.4f)*0.5f + 1f;
		}

		public static float OutIn (float k) {
			return k<.5f ? Elastic.Out(2f * k) * .5f : Elastic.In((2f * (k - .5f))) * .5f + 0.5f;
		}	
	};
	
	public class Back
	{
		static float s = 1.70158f;
		static float s2 = 2.5949095f;
		
		public static float In (float k) {
			return k*k*((s + 1f)*k - s);
		}
		
		public static float Out (float k) {
			return (k -= 1f)*k*((s + 1f)*k + s) + 1f;
		}
		
		public static float InOut (float k) {
			if ((k *= 2f) < 1f) return 0.5f*(k*k*((s2 + 1f)*k - s2));
			return 0.5f*((k -= 2f)*k*((s2 + 1f)*k + s2) + 2f);
		}

		public static float OutIn (float k) {
			return k<.5f ? Back.Out(2f * k) * .5f : Back.In((2f * (k - .5f))) * .5f + 0.5f;
		}		
	};
	
	public class Bounce
	{		
		public static float In (float k) {
			return 1f - Out(1f - k);
		}
		
		public static float Out (float k) {			
			if (k < (1f/2.75f)) {
				return 7.5625f*k*k;				
			}
			else if (k < (2f/2.75f)) {
				return 7.5625f*(k -= (1.5f/2.75f))*k + 0.75f;
			}
			else if (k < (2.5f/2.75f)) {
				return 7.5625f *(k -= (2.25f/2.75f))*k + 0.9375f;
			}
			else {
				return 7.5625f*(k -= (2.625f/2.75f))*k + 0.984375f;
			}
		}
		
		public static float InOut (float k) {
			if (k < 0.5f) return In(k*2f)*0.5f;
			return Out(k*2f - 1f)*0.5f + 0.5f;
		}	

		public static float OutIn (float k) {
			return k<.5f ? Bounce.Out(2f * k) * .5f : Bounce.In((2f * (k - .5f))) * .5f + 0.5f;
		}	
	};
}