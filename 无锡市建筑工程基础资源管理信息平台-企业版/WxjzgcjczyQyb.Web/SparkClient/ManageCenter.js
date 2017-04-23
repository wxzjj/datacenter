
	/*
	SetChecboxProperty函数，将argForm中的所有id包含子串argSubstrOfCheckboxID、类型为checkbox的html控件
	的checked属性设为和argSourceCheckbox这个控件的checked属性一致
	*/
	function SetCheckboxProperty(argForm,argSourceCheckbox,argSubstrOfCheckboxID)
	{
		for (i=0;i<argForm.length;i++)
		{
			var tempobj=argForm.elements[i]
			if ((tempobj.type.toLowerCase()=="checkbox") && (tempobj.id.toLowerCase().lastIndexOf(argSubstrOfCheckboxID.toLowerCase()) > 0))
				tempobj.checked = argSourceCheckbox.checked;
		}
	}
	
	/*
	AreAllChecked函数，判断argForm中的所有id包含子串argSubstrOfCheckboxID、类型为checkbox的html控件
	的checked属性是否都已设置
	*/
	function AreAllChecked(argForm,argSubstrOfCheckboxID)
	{
		var allChecked = true;
		for (i=0;i<argForm.length;i++)
		{
			var tempobj=argForm.elements[i]
			if ((tempobj.type.toLowerCase()=="checkbox") && (tempobj.id.toLowerCase().lastIndexOf(argSubstrOfCheckboxID.toLowerCase()) > 0))
				if (tempobj.checked == false)
				{
					allChecked = false;
					break;
				}
		}
		return(allChecked);
	}