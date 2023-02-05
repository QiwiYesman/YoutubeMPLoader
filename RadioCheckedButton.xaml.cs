using Microsoft.Maui.Controls;
using System;

namespace MauiApp1;

public partial class RadioCheckedButton : CheckedButton
{
	public string GroupName { get; set; } = "";
	public RadioCheckedButton()
	{
		InitializeComponent();
	}
	public override void SwitchCheck(object o, EventArgs e)
	{
		IsChecked = true;
        SetState();
		if (GroupName != "")
		{
			var parent = (Layout)Parent;
			foreach(var child in parent.Children)
			{
				if(child is RadioCheckedButton && ! ReferenceEquals(child, this))
				{
					var radio = (RadioCheckedButton)child;
					if(radio.GroupName == GroupName)
					{
						radio.IsChecked = false;
						radio.SetState();
					}
				}
			}
		}
				
		
	}
}