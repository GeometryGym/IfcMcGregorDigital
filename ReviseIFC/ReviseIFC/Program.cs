using GeometryGym.Ifc;
using System.Windows;
using System.Windows.Forms;

public class ReviseIFC
{
	[STAThread]
	static void Main(string[] args)
	{
		// 
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "IFC (*.ifc)|*.ifc";
		if (openFileDialog.ShowDialog() != DialogResult.OK)
			return;

		DatabaseIfc db = new DatabaseIfc(openFileDialog.FileName);
		foreach (IfcProduct product in db.OfType<IfcProduct>())
		{
			IfcPropertySingleValue psv = product.FindProperty("MANIFEST", true) as IfcPropertySingleValue;
			if(psv == null)
				psv = product.FindProperty("Manifest", true) as IfcPropertySingleValue;
			if (psv != null && psv.NominalValue != null)
				product.Name = psv.NominalValue.ValueString;
		}
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = openFileDialog.Filter;
		if (saveFileDialog.ShowDialog() != DialogResult.OK)
			return;
		db.WriteFile(saveFileDialog.FileName);
	}
}

