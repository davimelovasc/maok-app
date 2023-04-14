package crc644448ab6399bb57f3;


public class MaterialShapeViewRenderer
	extends crc644448ab6399bb57f3.MaterialContentViewRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("XamarinBackgroundKit.Android.Renderers.MaterialShapeViewRenderer, XamarinBackgroundKit.Android", MaterialShapeViewRenderer.class, __md_methods);
	}


	public MaterialShapeViewRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == MaterialShapeViewRenderer.class) {
			mono.android.TypeManager.Activate ("XamarinBackgroundKit.Android.Renderers.MaterialShapeViewRenderer, XamarinBackgroundKit.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public MaterialShapeViewRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == MaterialShapeViewRenderer.class) {
			mono.android.TypeManager.Activate ("XamarinBackgroundKit.Android.Renderers.MaterialShapeViewRenderer, XamarinBackgroundKit.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public MaterialShapeViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == MaterialShapeViewRenderer.class) {
			mono.android.TypeManager.Activate ("XamarinBackgroundKit.Android.Renderers.MaterialShapeViewRenderer, XamarinBackgroundKit.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
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
