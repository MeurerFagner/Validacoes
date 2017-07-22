using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WindowValidacao
{
    public static class Validacoes
    {
        public static bool ValidaObrigatorios(FrameworkElement elemento)
        {
            foreach (var item in LogicalTreeHelper.GetChildren(elemento))
            {
                if (item == null) continue;

                if (item.GetType().GetProperty("Text") != null)
                {

                    if (item is TextEditBase)
                    {
                        var texteditbase = item as TextEditBase;
                        if (string.IsNullOrEmpty(texteditbase.Text))
                        {
                            MessageBox.Show("Dados Inválidos");
                            DependencyObject buscaTab = texteditbase;
                            //BUSCA TAB ONDE O ITEM ESTÁ PARA GARANTIR QUE ELE IRA SER FOCADO
                            while (true)
                            {
                                buscaTab = LogicalTreeHelper.GetParent(buscaTab);
                                if (buscaTab == null)
                                {
                                    break;
                                }

                                if (buscaTab is DXTabItem)
                                {
                                    DXTabItem cotent = buscaTab as DXTabItem;
                                    cotent.IsSelected = true;
                                    cotent.UpdateLayout();
                                    break;
                                }
                            }

                            texteditbase.Focus();
                            return false;
                        }
                    }
                }
                else if (item is Panel)
                {
                    var panel = item as Panel;
                    if (!ValidaObrigatorios(panel)) return false;
                }
                else if (item is ItemsControl)
                {
                    var itemcontrol = item as ItemsControl;
                    if (!ValidaObrigatorios(itemcontrol)) return false;
                }
                else if (item is ContentControl)
                {
                    var contentControl = item as ContentControl;
                    if (!ValidaObrigatorios(contentControl)) return false;
                }
                else if (item is LayoutItem)
                {
                    var layoutItem = item as LayoutItem;
                    if (!ValidaObrigatorios(layoutItem)) return false;
                }
            }
            return true;
        }
    }
}
