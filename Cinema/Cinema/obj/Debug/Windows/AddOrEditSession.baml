   M S B A M L   `   `   `  ����A  =Cinema, Version=0.7.1.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CinemaCinema  5   	       Cinema.AddOrEditSession   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Cinema     5   	   -   5          � Title$  
Сеанс��5   	   $	��300��6   $	��520��6$    � 
ResizeMode$ NoResize=�6G   .��5	      � .+�5
   
   �� .+�5      � .+�5      �� 
����� �$��35��6(   �� 5      $��
СЕАНС��6   $��Center=�6)   $��Center=�6F   $��20(�6a   $
��Bold"�6o   5      � 5      $��Center=�6   $��10q�60   .+�5      �� .+�5      � $��0,0,0,10q�6   .+�5      �� $��Фильм:��6    $��16(�61   �� 5      -   5         ��Name$ SessionMovie��5   #   $
��Left=�69   $��	120,0,0,0q�6T   $	��350��6g   
�������$��16(�6�    �� SelectedIndex$ 0��6�   5      � 5      $��0,0,0,10q�6   .+�5      �� $��Дата сеанса:��6    $��16(�67   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ SessionDate��5   %   $
��Left=�6:   $��	120,0,0,0q�6U   $	��350��6h   $��16(�6t   5      � 5      $��0,0,0,10q�6   .+�5      �� $��Время сеанса:��6    $��16(�68   �� 5      -   5         ��Name$ SessionTime��5   "   $
��Left=�67   $��	120,0,0,0q�6R   $	��350��6e   $��16(�6q   5      � 5      $��0,0,0,10q�6   .+�5      �� $��Цена билета:��6    $��16(�67   �� 5      -   5        $ SessionTicketPrice��5   "   $
��Left=�6>   $��	120,0,0,0q�6Y   $	��350��6l   $��16(�6x   5       5!      5"      �� 5#      -   5         ��Name$
 Save��5#      $��0,0,0,0q�6#   $��Сохранить��64   $	��470��6H   $��16(�6T   
�������6u   5$      5%      5&      