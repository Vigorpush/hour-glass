using UnityEngine;

public class UIHealthbar : MonoBehaviour
{
	private UISlider _normal;
	private UISlider _normalDmg;
	private UISlider _shielded;
	private UISlider _shieldedDmg;
	private UISlider _poisoned;
	private UISprite _barSprite;
	private const float AnimDuration = 0.2f;
	private const iTween.EaseType EaseType = iTween.EaseType.linear;
	private bool _isFading;
	private float _timer;

	public bool autoHide = true;

	private bool IsVisible
	{
		get { return _barSprite.color.a >= 1; }
		set
		{
			_timer = 0;
			if (value != IsVisible && !_isFading)
			{
				_isFading = true;
				if (value)
				{
					iTween.ValueTo(gameObject,
						iTween.Hash("from", 0, "to", 1, "time", AnimDuration, "easetype",
							EaseType, "onupdate", "OnFadeIn",
							"onupdatetarget", gameObject, "oncomplete", "OnFadeInComplete", "oncompletetarget", gameObject));
				}
				else
				{
					iTween.ValueTo(gameObject,
						iTween.Hash("from", 1, "to", 0, "time", AnimDuration, "easetype",
							EaseType, "onupdate", "OnFadeOut",
							"onupdatetarget", gameObject, "oncomplete", "OnFadeOutComplete", "oncompletetarget", gameObject));
				}
			}
		}
	}

	private void Awake()
	{
		_normal = transform.FindChild("Normal").GetComponent<UISlider>();
		_normalDmg = transform.FindChild("NormalDmg").GetComponent<UISlider>();
		_shielded = transform.FindChild("Shielded").GetComponent<UISlider>();
		_shieldedDmg = transform.FindChild("ShieldedDmg").GetComponent<UISlider>();
		_poisoned = transform.FindChild("Poisoned").GetComponent<UISlider>();
		_barSprite = transform.GetComponent<UISprite>();

		if (autoHide)
		{
			_barSprite.color = new Color(1, 1, 1, 0);
		}
	}

	void OnEnable()
	{
		IsPoisoned = false;
		IsShielded = false;
		IsShieldedDamaging = false;
		IsNormalDamaging = false;
	}

	private void Update()
	{
		if (autoHide && !_isFading && IsVisible && !IsPoisoned)
		{
			_timer += Time.deltaTime;
			if (_timer > 5f)
			{
				IsVisible = false;
			}
		}
	}

	private bool IsNormalDamaging
	{
		get { return _normalDmg.gameObject.activeSelf; }
		set { _normalDmg.gameObject.SetActive(value); }
	}

	private bool IsShieldedDamaging
	{
		get { return _shieldedDmg.gameObject.activeSelf; }
		set { _shieldedDmg.gameObject.SetActive(value); }
	}


	public bool IsDamaging
	{
		get { return IsShieldedDamaging || IsNormalDamaging; }
	}


	public bool IsPoisoned
	{
		get { return _poisoned.gameObject.activeSelf; }
		private set { _poisoned.gameObject.SetActive(value); }
	}


	public bool IsShielded
	{
		get { return _shielded.gameObject.activeSelf; }
		private set { _shielded.gameObject.SetActive(value); }
	}

	/// <summary>
	/// Debug Testing
	/// </summary>
	private void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, 200, 100), "Hit - 20%"))
		{
			AddDamage(0.2f);
		}
		if (GUI.Button(new Rect(10, 120, 200, 100), "Shield + 30%(3s)"))
		{
			AddShield(0.3f, 3);
		}
		if (GUI.Button(new Rect(10, 230, 200, 100), "Poison + 10%(5%/s)"))
		{
			AddPoison(0.1f, 0.05f);
		}
	}


	public void AddShield(float percent, float time)
	{
		if (!IsVisible)
		{
			IsVisible = true;
		}


		if (_normal.value + percent > 1f)
		{
			var total = _normal.value + percent;
			_normal.value /= total;
			percent /= total;
		}


		if (IsPoisoned)
		{

			if (percent > _poisoned.value)
			{
				percent -= _poisoned.value;
			}
			else //否则
			{
				percent = percent - _poisoned.value;
			}
		}
		_shielded.value = _normal.value + percent;
		IsShielded = true;
		iTween.ValueTo(gameObject,
			iTween.Hash("from", 0, "to", time, "time", time, "onupdate", "OnShield", "oncomplete", "ShieldTimeOut",
				"oncompletetarget", gameObject));
	}


	public void AddPoison(float percent, float speed)
	{
		if (percent <= 0)
		{
			return;
		}

		if (!IsVisible)
		{
			IsVisible = true;
		}


		if (IsShielded)
		{
			_shielded.value -= percent;
		}

		_normal.value -= percent;
		IsPoisoned = true;
		iTween.ValueTo(gameObject,
			iTween.Hash("from", percent, "to", 0, "speed", speed, "easetype", EaseType, "onupdate", "OnPoison", "onupdatetarget",
				gameObject,
				"oncomplete",
				"PoisonTimeOut",
				"oncompletetarget", gameObject));
	}


	public float AddDamage(float percent)
	{
		if (!IsVisible)
		{
			IsVisible = true;
		}
		if (percent > 1f)
		{
			Debug.LogError(string.Format("Illegal damage percent: -{0}", percent));
			return _normal.value;
		}
		if (_normal.value <= 0f)
		{
			Debug.LogError(string.Format("Health is already below zero: -{0}", percent));
			return _normal.value;
		}
		if (IsShielded)
		{
			_shieldedDmg.value = _shielded.value;
			_shielded.value -= percent;
			IsShieldedDamaging = true;
			iTween.ValueTo(gameObject,
				iTween.Hash("from", _shieldedDmg.value, "to", _shielded.value, "time", AnimDuration, "easetype",
					EaseType, "onupdate", "OnShieldedDamage",
					"onupdatetarget", gameObject, "oncomplete", "ShieldedDamageDone", "oncompletetarget", gameObject));
			//if damage lows the shield value to zero, take health instead
			//...
		}
		else
		{
			_normalDmg.value = _normal.value;
			_normal.value -= percent;
			IsNormalDamaging = true;
			iTween.ValueTo(gameObject,
				iTween.Hash("from", _normalDmg.value, "to", _normal.value, "time", AnimDuration, "easetype", EaseType,
					"onupdate", "OnNormalDamage",
					"onupdatetarget", gameObject, "oncomplete", "NormalDamageDone", "oncompletetarget", gameObject));
		}

		return _normal.value;
	}

	private void OnNormalDamage(float value)
	{
		_normalDmg.value = value;
	}

	private void NormalDamageDone()
	{
		IsNormalDamaging = false;
	}

	private void OnShieldedDamage(float value)
	{
		_shieldedDmg.value = value;
	}

	private void ShieldedDamageDone()
	{
		IsShieldedDamaging = false;
	}

	private void OnShield(float value)
	{
	}

	private void ShieldTimeOut()
	{
		IsShielded = false;
	}

	private void OnPoison(float value)
	{
		_poisoned.value = _normal.value + value;
	}

	private void PoisonTimeOut()
	{
		IsPoisoned = false;
		_poisoned.value = _normal.value;
	}

	private void OnFadeIn(float value)
	{
		_barSprite.color = new Color(1, 1, 1, value);
	}

	private void OnFadeInComplete()
	{
		_isFading = false;
		_timer = 0;
	}

	private void OnFadeOut(float value)
	{
		_barSprite.color = new Color(1, 1, 1, value);
	}

	private void OnFadeOutComplete()
	{
		_isFading = false;
		_timer = 0;
	}
}