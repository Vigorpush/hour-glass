using System.Reflection;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class UIXMLEditor : ScriptableObject
{
	[MenuItem("Tool/UI xml export")]
	private static void MenuExport()
	{
		UIRoot root = Selection.activeGameObject.GetComponent<UIRoot>();
		if (root == null)
		{
			Debug.LogError("No UIRoot selected!");
			return;
		}

		XmlDocument xdoc = new XmlDocument();
		XmlElement re = xdoc.CreateElement("Root");
		xdoc.AppendChild(re);

		GetChild(root.transform, xdoc, re);

		xdoc.Save("Assets/Resources/UI/uiconfig.xml");
		Debug.Log("Saved!");
	}

	private static void GetChild(Transform transform, XmlDocument xdoc, XmlElement re)
	{
		XmlElement te = AddTransformAttribute(transform, xdoc, re);
		AddComponent(transform, xdoc, te);

		foreach (Transform child in transform)
		{
			GetChild(child, xdoc, te);
		}
	}

	private static void AddComponent(Transform transform, XmlDocument xdoc, XmlElement el)
	{
		foreach (MonoBehaviour component in transform.GetComponents<MonoBehaviour>())
		{
			XmlElement ce = xdoc.CreateElement("Component");
			ce.SetAttribute("type", component.GetType().ToString());
			el.AppendChild(ce);

			//switch (component.GetType().ToString())
			//{
			//	case "UIRoot":
			//		SetUIRoot(ce, component as UIRoot);
			//		break;
			//	case "UIPanel":
			//		SetUIPanel(ce, component as UIPanel);
			//		break;
			//	case "UICamera":
			//		SetUICamera(ce, component as UICamera);
			//		break;
			//	case "UISprite":
			//		SetUISprite(ce, component as UISprite);
			//		break;
			//	case "UIButton":
			//		SetUIButton(ce, component as UIButton);
			//		break;
			//	case "UIPlaySound":
			//		SetUIPlaySound(ce, component as UIPlaySound);
			//		break;
			//	case "UILabel":
			//		SetUILabel(ce, component as UILabel);
			//		break;
			//}

			SetEverything(ce, component);
		}
	}

	private static void SetUILabel(XmlElement el, UILabel comp)
	{
		el.SetAttribute("bitmapFont", comp.bitmapFont.ToString());
		el.SetAttribute("fontSize", comp.fontSize.ToString());
		el.SetAttribute("text", comp.text);
		el.SetAttribute("overflowMethod", comp.overflowMethod.ToString());
		el.SetAttribute("alignment", comp.alignment.ToString());
		el.SetAttribute("gradientTop", comp.gradientTop.ToString());
		el.SetAttribute("gradientBottom", comp.gradientBottom.ToString());
		el.SetAttribute("applyGradient", comp.applyGradient.ToString());
		el.SetAttribute("effectStyle", comp.effectStyle.ToString());
		el.SetAttribute("effectColor", comp.effectColor.ToString());
		el.SetAttribute("effectDistance.x", comp.effectDistance.x.ToString());
		el.SetAttribute("effectDistance.y", comp.effectDistance.y.ToString());
		el.SetAttribute("spacingX", comp.spacingX.ToString());
		el.SetAttribute("spacingY", comp.spacingY.ToString());
		el.SetAttribute("maxLineCount", comp.maxLineCount.ToString());
		el.SetAttribute("color", comp.color.ToString());
	}

	private static void SetUIPlaySound(XmlElement el, UIPlaySound comp)
	{
		el.SetAttribute("audioClip", comp.audioClip.ToString());
		el.SetAttribute("audioClip", comp.trigger.ToString());
		el.SetAttribute("volume", comp.volume.ToString());
		el.SetAttribute("pitch", comp.pitch.ToString());
	}

	private static void SetUIButton(XmlElement el, UIButton comp)
	{
		el.SetAttribute("tweenTarget", comp.tweenTarget.ToString());
		el.SetAttribute("normalSprite", comp.normalSprite);
		el.SetAttribute("hoverSprite", comp.hoverSprite);
		el.SetAttribute("pressedSprite", comp.pressedSprite);
		el.SetAttribute("disabledSprite", comp.disabledSprite);
	}

	private static void SetUISprite(XmlElement el, UISprite comp)
	{
		el.SetAttribute("atlas", comp.atlas.ToString());
		el.SetAttribute("spriteName", comp.spriteName);
		el.SetAttribute("type", comp.type.ToString());
		el.SetAttribute("flip", comp.flip.ToString());
		el.SetAttribute("centerType", comp.centerType.ToString());
		el.SetAttribute("color", comp.color.ToString());
		el.SetAttribute("depth", comp.depth.ToString());
		el.SetAttribute("localSize.x", comp.localSize.x.ToString());
		el.SetAttribute("localSize.y", comp.localSize.y.ToString());
	}

	private static void SetUICamera(XmlElement el, UICamera comp)
	{
		el.SetAttribute("eventType", comp.eventType.ToString());
		el.SetAttribute("eventReceiverMask", comp.eventReceiverMask.ToString());
	}

	private static void SetUIPanel(XmlElement el, UIPanel comp)
	{
		el.SetAttribute("alpha", comp.alpha.ToString());
		el.SetAttribute("depth", comp.depth.ToString());
		el.SetAttribute("clipping", comp.clipping.ToString());
	}

	private static void SetUIRoot(XmlElement el, UIRoot comp)
	{
		el.SetAttribute("scalingStyle", comp.scalingStyle.ToString());
		el.SetAttribute("minimumHeight", comp.minimumHeight.ToString());
		el.SetAttribute("maximumHeight", comp.maximumHeight.ToString());
		el.SetAttribute("manualHeight", comp.manualHeight.ToString());
		el.SetAttribute("shrinkPortraitUI", comp.shrinkPortraitUI.ToString());
		el.SetAttribute("adjustByDPI", comp.adjustByDPI.ToString());
	}

	private static void SetEverything(XmlElement el, MonoBehaviour comp)
	{
		foreach (FieldInfo fieldInfo in comp.GetType().GetFields())
		{
			string value = fieldInfo.GetValue(comp) != null ? fieldInfo.GetValue(comp).ToString() : null;
			el.SetAttribute(fieldInfo.Name, value);
		}
	}

	private static XmlElement AddTransformAttribute(Transform transform, XmlDocument xdoc, XmlElement el)
	{
		XmlElement te = xdoc.CreateElement("Transform");
		el.AppendChild(te);
		te.SetAttribute("name", transform.name);

		XmlElement pe = xdoc.CreateElement("position");
		te.AppendChild(pe);
		pe.SetAttribute("x", transform.position.x.ToString());
		pe.SetAttribute("y", transform.position.y.ToString());
		pe.SetAttribute("z", transform.position.z.ToString());

		XmlElement re = xdoc.CreateElement("rotation");
		te.AppendChild(re);
		re.SetAttribute("x", transform.rotation.x.ToString());
		re.SetAttribute("y", transform.rotation.y.ToString());
		re.SetAttribute("z", transform.rotation.z.ToString());

		XmlElement se = xdoc.CreateElement("localScale");
		te.AppendChild(se);
		se.SetAttribute("x", transform.localScale.x.ToString());
		se.SetAttribute("y", transform.localScale.y.ToString());
		se.SetAttribute("z", transform.localScale.z.ToString());

		return te;
	}
}