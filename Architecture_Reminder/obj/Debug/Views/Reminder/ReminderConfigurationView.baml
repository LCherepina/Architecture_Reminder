   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       Architecture_ReminderD   >Architecture_Reminder.Views.Reminder.ReminderConfigurationView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      .��5
      �   � RowDefinitions  5   
   �� 5      $��70 �6   6*    � ColumnDefinitions 5   
   �� 5      $	��100 �6   6-   �� 5      $	��100 �6   6-   �� 5      $	��210��6   60   .+�5   
   (  "System.Windows.Controls.DatePicker  -   5        $��
DatePicker��5      |�.  $��0��6<   $��0��6I   $��Center=�6Y   $��Center=�6t   
 �� Now   DisplayDateStart# Z 6�      SelectedDate 6�   �� *	RemDate+ �� UpdateSourceTrigger$ PropertyChanged=�6  �� 5   
   -   5        $��ComboBoxHours��5      $��0��6(   $��1��65   $��40��6E   $��Center=�6P   $
��Left=�6k   $��5,0,0,0q�6�   ��6�   �� *	FillHours+ �� SelectedIndex 6�   �� *RemTimeHours+ �� Mode$ TwoWay=�$ PropertyChanged=� �� Delay$ 5��5      �� 5   
   $��0��6   $��:��6   $��1��6*   $��Center=�6;   $��Center=�6V   6u   �� 5   
   -   5        $��ComboBoxMinutes��5      $��0��6*   $��1��67   $��40��6G   $��Center=�6R   $��Right=�6m   $��0,0,5,0q�6�   ��6�   �� *FillMinutes+ 6�   �� *RemTimeMinutes+$ TwoWay=�$ PropertyChanged=�$ 5��5      �� 5   
   -   5        $
��Text��5      $��0��6   $��2��6+   $��Center=�6;   $��5,0,5,0 q�6V   $��60��6h   	 �� TextWrapping$
	 Wrap=�6t   
 �� AcceptsReturn
 .  �� IsUndoEnabled . " �� VerticalScrollBarVisibility$
 Auto=�5      ��65   �� *	RemText+$ PropertyChanged=�.��6s   5       5"      5#      