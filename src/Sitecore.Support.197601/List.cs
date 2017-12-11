using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public class List : ListControl
  {
    private static readonly string baseCssClassName = "scfListBoxBorder";

    protected ListBox list = new ListBox();

    [VisualCategory("List"), VisualFieldType(typeof(SelectionModeField)), VisualProperty("Selection Mode:", 400), DefaultValue("Single")]
    public string SelectionMode
    {
      get
      {
        return this.list.SelectionMode.ToString();
      }
      set
      {
        this.list.SelectionMode = (ListSelectionMode)Enum.Parse(typeof(ListSelectionMode), value, true);
      }
    }

    [VisualProperty("Rows:", 450), DefaultValue(4)]
    public int Rows
    {
      get
      {
        return this.list.Rows;
      }
      set
      {
        this.list.Rows = value;
      }
    }

    public override string ID
    {
      get
      {
        return this.list.ID;
      }
      set
      {
        this.title.ID = value + "text";
        this.list.ID = value;
        base.ID = value + "scope";
      }
    }

    protected override System.Web.UI.WebControls.ListControl InnerListControl
    {
      get
      {
        return this.list;
      }
    }

    [VisualFieldType(typeof(CssClassField)), VisualProperty("CSS Class:", 600), DefaultValue("scfListBoxBorder")]
    public new string CssClass
    {
      get
      {
        return base.CssClass;
      }
      set
      {
        base.CssClass = value;
      }
    }

    public List() : this(HtmlTextWriterTag.Div)
    {
    }

    private List(HtmlTextWriterTag tag) : base(tag)
    {
      this.CssClass = List.baseCssClassName;
      this.list.Rows = 4;
    }

    protected override void OnInit(EventArgs e)
    {
      base.KeepHiddenValue = false;
      base.OnInit(e);
      this.list.CssClass = "scfListBox";
      this.help.CssClass = "scfListBoxUsefulInfo";
      this.title.CssClass = "scfListBoxLabel";
      this.generalPanel.CssClass = "scfListBoxGeneralPanel";
      this.Controls.AddAt(0, this.generalPanel);
      this.Controls.AddAt(0, this.title);
      this.generalPanel.Controls.AddAt(0, this.help);
      this.generalPanel.Controls.AddAt(0, this.list);
    }
  }
}