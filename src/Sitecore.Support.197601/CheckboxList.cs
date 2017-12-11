﻿using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using Sitecore.Forms.Core.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [ValidationProperty("Value")]
  public class CheckboxList : ListControl
  {
    private static readonly string baseCssClassName = "scfCheckBoxListBorder";

    protected CheckBoxList buttonList = new CheckBoxList();

    [VisualFieldType(typeof(DirectionField)), VisualProperty("Direction:", 300)]
    public string Direction
    {
      get
      {
        return this.buttonList.RepeatDirection.ToString();
      }
      set
      {
        this.buttonList.RepeatDirection = (RepeatDirection)Enum.Parse(typeof(RepeatDirection), value, true);
      }
    }

    [VisualFieldType(typeof(EditField)), VisualProperty("Columns:", 400), DefaultValue(1)]
    public string Columns
    {
      get
      {
        return this.buttonList.RepeatColumns.ToString();
      }
      set
      {
        int repeatColumns;
        if (!int.TryParse(value, out repeatColumns))
        {
          repeatColumns = 1;
        }
        this.buttonList.RepeatColumns = repeatColumns;
      }
    }

    public RepeatLayout RepeatLayout
    {
      get
      {
        return this.buttonList.RepeatLayout;
      }
      set
      {
        this.buttonList.RepeatLayout = value;
      }
    }

    [Localize, VisualCategory("List"), VisualFieldType(typeof(MultipleSelectedValueField)), VisualProperty("Selected Value:", 200), Browsable(false), TypeConverter(typeof(ListItemCollectionConverter))]
    public new ListItemCollection SelectedValue
    {
      get
      {
        return this.selectedItems;
      }
      set
      {
        this.selectedItems = value;
      }
    }

    public override string ID
    {
      get
      {
        return base.ID;
      }
      set
      {
        this.title.ID = value + "text";
        this.buttonList.ID = value + "list";
        base.ID = value;
      }
    }

    [VisualFieldType(typeof(CssClassField)), VisualProperty("CSS Class:", 600), DefaultValue("scfCheckBoxListBorder")]
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

    protected override System.Web.UI.WebControls.ListControl InnerListControl
    {
      get
      {
        return this.buttonList;
      }
    }

    public string Value
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (ListItem listItem in this.InnerListControl.Items)
        {
          if (listItem.Selected)
          {
            stringBuilder.AppendFormat("<item>{0}</item>", listItem.Value);
          }
        }
        return stringBuilder.ToString();
      }
    }

    public CheckboxList() : this(HtmlTextWriterTag.Div)
    {
    }

    public CheckboxList(HtmlTextWriterTag tag) : this(DependenciesManager.RequirementsChecker, tag)
    {
    }

    public CheckboxList(IRequirementsChecker requirementsChecker, HtmlTextWriterTag tag) : this(requirementsChecker, tag, new ListFieldValueFormatter(DependenciesManager.Resolve<ISettings>()))
    {
    }

    public CheckboxList(IRequirementsChecker requirementsChecker, HtmlTextWriterTag tag, ListFieldValueFormatter listFieldValueFormatter) : base(requirementsChecker, tag, listFieldValueFormatter)
    {
      this.CssClass = CheckboxList.baseCssClassName;
      this.buttonList.RepeatColumns = 1;
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.buttonList.CssClass = "scfCheckBoxList";
      this.help.CssClass = "scfCheckBoxListUsefulInfo";
      this.title.CssClass = "scfCheckBoxListLabel";
      this.generalPanel.CssClass = "scfCheckBoxListGeneralPanel";
      this.Controls.AddAt(0, this.generalPanel);
      this.Controls.AddAt(0, this.title);
      this.generalPanel.Controls.AddAt(0, this.help);
      this.generalPanel.Controls.AddAt(0, this.buttonList);
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.title.AssociatedControlID = null;
    }
  }
}