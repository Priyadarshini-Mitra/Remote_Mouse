   M S B A M L   `   `   `  ����  WpfPortOfTestingCamera*    $WpfPortOfTestingCamera.VideoSettings   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   �   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�     � Title$  Video Settings��6	    � WindowStartupLocation$ CenterOwner=�6     � 
ResizeMode$ NoResize=�6D    � ShowInTaskbar .   � SizeToContent$ WidthAndHeight=�6   J� SnapsToDevicePixels . .��5      �� -   5        $��stackPanel1��5      $	��Top=�6$   $��Center=�6<   .+�5   
   �� -   5        $��	groupBox1��5   %   $��Settings��6   .��5      � -   5        $��grid1��5      $��Center=�6    $��Center=�6;    � ColumnDefinitions 5      �� 5	      $	��80* �6'   �� 5
      $
��175* �6'    � RowDefinitions 5      �� 5      �� 5      .+�5      �� -   5        $��label1��5   :   $��Resolution:��6   $��28��6.   $��0q�6H   $
��Left=�6S   $��Center=�6n   �� 5      -   5        $��label2��5   _   $��
Framerate:��6   $��28��6-   $
��Left=�69   $��0q�6T   $��Center=�6m   $��1��6�   �� 5      -   5        $��	comboBox1��5   ]   $��1��6   $��23��6+   $
��Left=�67   $��0q�6R   $��Center=�6n   $	��150��6�   6�   �� 5      -   5        $��	comboBox2��5   M   $��23��6   $
��Left=�6'   $��0q�6B   $��Center=�6^   $	��150��6y   $��1��6�   $��1��6�   {�.  6�   5      5      �� 5   
   -   5        $��labelSelectedSize��5      $��
Size @ FPS��6)   �� 5   
   -	   5        $��button1��5      $��Apply��6    60   5      5      