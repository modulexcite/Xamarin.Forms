using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Widget;

namespace Xamarin.Forms.Platform.Android
{
	internal sealed class ToolbarImageButton : ImageButton, IToolbarButton
	{
		public ToolbarImageButton(Context context, ToolbarItem item) : base(context)
		{
			if (item == null)
				throw new ArgumentNullException("item", "you should specify a ToolbarItem");
			Item = item;
			Enabled = item.IsEnabled;
			Bitmap bitmap;
			bitmap = Context.Resources.GetBitmap(Item.Icon);
			SetImageBitmap(bitmap);
			SetBackgroundColor(new Color(0, 0, 0, 0).ToAndroid());
			Click += (sender, e) => item.Activate();
			bitmap.Dispose();
			Item.PropertyChanged += HandlePropertyChanged;
		}

		public ToolbarItem Item { get; set; }

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				Item.PropertyChanged -= HandlePropertyChanged;
			base.Dispose(disposing);
		}

		void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == MenuItem.IsEnabledProperty.PropertyName)
				Enabled = Item.IsEnabled;
		}
	}
}