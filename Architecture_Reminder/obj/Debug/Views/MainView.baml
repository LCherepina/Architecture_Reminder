   M S B A M L   `   `   `  ����  Architecture_Reminder*    $Architecture_Reminder.Views.MainView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5	      � -   5        $��	MainGrid1��5	      $
��Auto��6   .+�5
   
   � -   5        $��Grid11��5
      $��0,10,0,0q�6     � ColumnDefinitions  5      �� 5      $��70 �6#   60   �� 5      -   5         �Name$
 Rems��5   #   $��* �6/    � RowDefinitions 5      �� 5      $��50 �6    6.   �� 5      $��50 �6    6.   �� 5      $��50 �6    6.   �� 5      $��50 �6    6.   �� 5      $��50 �6    6.   �� 5      $��50 �6    6.   �� 5      $��50 �6    6.   �� 5      $��50 �6    6.   .+�5      �� $	��Add��6   $
��Bold"�6#   $��0��6]   $��0��6j   $��	6,10,6,10q�6z   ��65   �� *AddReminderCommand+6�   �� 5      $��Delete��6   $
��Bold"�6&   $��1��6b   $��0��6o   $��	6,10,6,10q�6   ��68   �� *DeleteReminderCommand+6�   �� 5      $
��Sort��6   $
��Bold"�6$   $��2��6_   $��0��6l   $��	6,10,6,10q�6|   ��66   �� *SortReminderCommand+6�   �� 5      $��Log Out��6   $
��Bold"�6'   $��7��6\   $��0��6i   $��	5,10,5,10q�6y   ��69   �� *LogOutCommand+6�   5       � 5#   
   -   5        $��MainGrid��5#      $��	70,10,0,0q�6   .+�5$      �� -   5        $��ListBoxMain��5$      $��0q�6)   $��Stretch=�6=   ��.  $��Disabled=�6�    �� SelectedIndex 6b   �� *SelectedReminderIndex+5%      5&      5(      5*      