   M S B A M L   `   `   `  ����m  iSystem.Windows.Forms.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35�mclr-namespace:System.Windows.Forms.DataVisualization.Charting;assembly=System.Windows.Forms.DataVisualization/System.Windows.Forms.DataVisualization.Charting  5      H DTrendViewTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TrendViewTest.UITrendViewTest.UI 5      "   TrendViewTest.UI.TrendUCView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      �winformchartmclr-namespace:System.Windows.Forms.DataVisualization.Charting;assembly=System.Windows.Forms.DataVisualization     5      .tvtclr-namespace:TrendViewTest.UI     5      .��5	      � -   5          �Name$
  grid��5	      .+�5
   
   7  1System.Windows.Forms.Integration.WindowsFormsHost  -   5          Name$
 host��5
   N   $
��Left=�6   $	��Top=�66   $	��290��6\   $	��972��6i   $��	10,10,0,0q�6u      Child. 5      ;   5System.Windows.Forms.DataVisualization.Charting.Chart     BorderlineColorU QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a#  System.Drawing.ColorConverter$ Black 6!   -   5        cpuChart��5   9   5        5   
   -   5        $ host1��5   N   $
��Left=�6   $	��Top=�66   $��30��6]   $	��972��6i   $��
10,305,0,0q�6u   . 5        -   5        	cpuChart2��5   !   5      &   TrendViewTest.UI.ComboBoxControl  5   
   -   5          Name$
 cmbc��5   1   
�������$
��Left=�6?   $
��Auto��6Z   $	��Top=�6h   $��364,470,0,0q�6�   �� 5   
   -   5         ��Name$	 btn��5      $
��Left=�6   $	��Top=�69   $��Başlat��6Q   $	��250��6w   $��30��6�   $��326,340,0,0q�6�   �� 5   
    �� Orientation$ 
Horizontal=�6   60   �� 5   
   -   5         ��Name$	 cmb��5      $	��200��6    $��30��6,    �� SelectedIndex$ 4��68   $
��Left=�6J   $	��Top=�6e   $��682,340,0,0q�6}   6�   �� 5   
   -	   5        
������ $
��Left=�5   '   $	��Top=�6B   	 J� 	AllowDrop	 . $	��290��6�   $	��972��6�   $��	10,10,0,0q�6�   � 5   
   -
   5        $  gridData��5      $
��Left=�6!   $	��Top=�6<   $��1020,10,0,0q�6T   $	��336��6i   .+�5      �� -   5        
 ��Name$
 
scrollData��5      $
��Auto=�6/   .��5      �� -   5         ��Name$ 	panelData��5      $ Vertical=�60   $y�	Collapsed=�6G   .+�5      !  TrendViewTest.UI.DataCellUC  6'   5      5      5      5      5      