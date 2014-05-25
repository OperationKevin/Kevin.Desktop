using System;
using Gtk;
using Kevin.Core;

public partial class MainWindow: Gtk.Window, Output
{
	KevinCore core;
	TextTag kevinTag;
	TextTag yourTag;
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		core = new KevinCore (this);

		kevinTag = new TextTag (null);
		resultTextView.Buffer.TagTable.Add (kevinTag);
		kevinTag.Foreground = "#55AA00";

		yourTag = new TextTag (null);
		resultTextView.Buffer.TagTable.Add (yourTag);
		yourTag.Foreground = "#FF44BB";

		send ("Hello :D");

	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	#region Output implementation

	public void send (string hey)
	{

		var iter = this.resultTextView.Buffer.EndIter;
		resultTextView.Buffer.InsertWithTags (iter, "Kevin: " + hey + "\n", kevinTag);
		resultTextView.ScrollToIter(resultTextView.Buffer.EndIter, 0, false, 0, 0); 

	}

	#endregion

	protected void OnEntry3Activated (object sender, EventArgs e)
	{
		var iter = this.resultTextView.Buffer.EndIter;
		string query = ((Gtk.Entry)sender).Text;
		resultTextView.Buffer.InsertWithTags(iter, "You: " + query + "\n", yourTag);
		((Gtk.Entry)sender).Text = "";
		resultTextView.ScrollToIter(resultTextView.Buffer.EndIter, 0, false, 0, 0); 
		core.tell (query);
	}
}
