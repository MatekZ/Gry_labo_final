using System;
using UnityEngine;

public class QuestionDialog : MonoBehaviour
{
	public event Action OnYesEvent;
	public event Action OnNoEvent;
	public StatPanel stat;

	public void Show()
	{
		this.gameObject.SetActive(true);
		OnYesEvent = null;
		OnNoEvent = null;
	}

	public void Hide()
	{
		this.gameObject.SetActive(false);
	}

	public void OnYesButtonClick()
	{
		if (OnYesEvent != null)
			OnYesEvent();
		stat.UpdateStatValues();
		stat.UpdateStatNames();
		Hide();
	}

	public void OnNoButtonClick()
	{
		if (OnNoEvent != null)
			OnNoEvent();
        stat.UpdateStatValues();
        stat.UpdateStatNames();
        Hide();
	}
}
