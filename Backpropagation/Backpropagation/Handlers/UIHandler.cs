using System.Collections.Generic;
using System.Windows.Forms;

namespace Backpropagation.Handlers
{
	public static class UiHandler
	{
		public static void PanelVisible(Panel visible, List<Panel> panels)
		{
			foreach (Panel panel in panels)
			{
				panel.Visible = panel.Equals(visible);
			}
		}
	}
}