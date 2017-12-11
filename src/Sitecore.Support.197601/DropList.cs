using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public class DropList : ListControl
  {
    private static readonly string baseCssClassName = "scfDropListBorder";

    protected DropDownList droplist = new DropDownList();

    private bool emptyLine = true;

    [VisualCategory("List"), VisualFieldType(typeof(EmptyChoiceField)), VisualProperty("Empty Choice:", 300), DefaultValue("Yes")]
    public string EmptyChoice
    {
      get
      {
        return this.emptyLine.ToString();
      }
      set
      {
        this.emptyLine = (value == "Yes");
      }
    }

    public override string ID
    {
      get
      {
        return this.droplist.ID;
      }
      set
      {
        this.title.ID = value + "text";
        this.droplist.ID = value;
        base.ID = value + "scope";
      }
    }

    public override ControlResult Result
    {
      get
      {
        return new ControlResult(this.ControlName, this.InnerListControl.SelectedValue ?? string.Empty, (this.InnerListControl.SelectedItem != null) ? this.InnerListControl.SelectedItem.Text : string.Empty);
      }
    }

    protected override System.Web.UI.WebControls.ListControl InnerListControl
    {
      get
      {
        return this.droplist;
      }
    }

    [VisualFieldType(typeof(CssClassField)), VisualProperty("CSS Class:", 600), DefaultValue("scfDroplistBorder")]
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

    public DropList() : this(HtmlTextWriterTag.Div)
    {
    }

    private DropList(HtmlTextWriterTag tag) : base(tag)
    {
      this.CssClass = DropList.baseCssClassName;
    }

    protected override void InitItems(ListItemCollection items)
    {
      base.KeepHiddenValue = false;
      if (this.emptyLine)
      {
        if (items == null)
        {
          items = new ListItemCollection();
        }
        if (items.FindByText(string.Empty) == null)
        {
          items.Insert(0, new ListItem(string.Empty));
        }
      }
      base.InitItems(items);
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.droplist.CssClass = "scfDropList";
      this.help.CssClass = "scfDropListUsefulInfo";
      this.title.CssClass = "scfDropListLabel";
      this.generalPanel.CssClass = "scfDropListGeneralPanel";
      this.Controls.AddAt(0, this.generalPanel);
      this.Controls.AddAt(0, this.title);
      this.generalPanel.Controls.AddAt(0, this.help);
      this.generalPanel.Controls.AddAt(0, this.droplist);
    }
  }
}