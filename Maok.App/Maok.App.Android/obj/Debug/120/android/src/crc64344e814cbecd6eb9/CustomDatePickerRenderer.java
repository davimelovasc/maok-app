package crc64344e814cbecd6eb9;


public class CustomDatePickerRenderer
	extends crc643f46942d9dd1fff9.DatePickerRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Maok.App.Droid.CustomDatePickerRenderer, Maok.App.Android", CustomDatePickerRenderer.class, __md_methods);
	}


	public CustomDatePickerRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CustomDatePickerRenderer.class) {
			mono.android.TypeManager.Activate ("Maok.App.Droid.CustomDatePickerRenderer, Maok.App.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public CustomDatePickerRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CustomDatePickerRenderer.class) {
			mono.android.TypeManager.Activate ("Maok.App.Droid.CustomDatePickerRenderer, Maok.App.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public CustomDatePickerRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CustomDatePickerRenderer.class) {
			mono.android.TypeManager.Activate ("Maok.App.Droid.CustomDatePickerRenderer, Maok.App.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
