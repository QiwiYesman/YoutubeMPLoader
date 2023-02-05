using Microsoft.Maui.Controls;
using System;

namespace MauiApp1;

public partial class CheckedButton : Button
{
	protected bool is_checked = false;
	public virtual event Action<bool>? OnChecked = delegate { };
    virtual public bool IsChecked {
		get { return is_checked; }
		set
		{
			is_checked = value;
            SetState();
            InvokeOnCheckedEvent(is_checked);
			
		}
	}
	public CheckedButton() :base()
	{
		InitializeComponent();
		SetState();
	}
	public virtual void SwitchCheck(object o, EventArgs e)
	{ 		
		IsChecked = !IsChecked;
		SetState();

	}
	void InvokeOnCheckedEvent(bool value)
	{
        OnChecked.Invoke(value);
    }
	protected void SetState()
	{
		string visualState = IsChecked ? "IsChecked" : "Normal";
        VisualStateManager.GoToState(this, visualState);
    }

}