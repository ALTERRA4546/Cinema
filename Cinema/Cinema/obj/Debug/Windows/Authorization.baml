   M S B A M L   `   `   `  ����A  =Cinema, Version=0.7.1.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CinemaCinema  5   	       Cinema.Authorization   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Cinema     5   	   -   5          � Title$  Авторизация��5   	   $	��240��6   $	��400��6*    � 
ResizeMode$ NoResize=�66   .��5	      � .+�5
   
   �� .+�5      � $��0,0,0,10q�6   .+�5      �� 
����� �$��35��6(   �� 5      $��АВТОРИЗАЦИЯ��6   $��Center=�6/   $��Center=�6L   $��20(�6g   $
��Bold"�6u   5      �� 5      $��Center=�6   .+�5      � .+�5      �� $��Имя БД��6   $
��Left=�6-   $��0,0,0,0q�6H   $��Center=�6Y   $��16(�6t   �� 5      -   5         ��Name$ DataBaseConnection��5      $
��Left=�6:   $��80,0,0,0q�6U   $��Center=�6g   $	��250��6�   $��24��6�   $��16(�6�   6�   5      � 5      .+�5      �� $��
Логин��6   $
��Left=�6,   $��0,0,0,0q�6G   $��Center=�6X   $��16(�6s   �� 5      -   5        $ Login��5      $
��Left=�6-   $��80,0,0,0q�6H   $��Center=�6Z   $	��250��6u   $��24��6�   $��16(�6�   6�   5      � 5      .+�5      �� $��Пароль��6   $
��Left=�6-   $��0,0,0,0q�6H   $��Center=�6Y   $��16(�6t   �� 5      -   5        $ Password��5      $
��Left=�60   $��80,0,0,0q�6K   $��Center=�6]   $	��250��6x   $��24��6�   $��16(�6�   6�   5      �� 5      -   5         ��Name$ Enter��5      $��
Войти��6(   $��0,10,0,0q�68   $	��200��6J   $��16(�6V   $��Center=�6d   
�������6�   5      5      � 5    
   $��Bottom=�6   .+�5!      �� $��Pre-release v0.7.5��6   $
��Left=�61   �� 5"      $��Early access��6   $��Right=�6+   5#      � 5%   
   -   5         �Name$ LoadView��5%      $��5��6(   $��Bottom=�63    � IsIndeterminate . $y�	Collapsed=�6e   
����� �6|   5&      5'      