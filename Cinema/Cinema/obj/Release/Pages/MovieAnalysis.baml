   M S B A M L   `   `   `  ���� A  =Cinema, Version=0.7.1.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CinemaCinema      Cinema.MovieAnalysis   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     &localclr-namespace:Cinema     -     N� Title$  TicketAnalysis��.��� 
�������.+��� .+�� 
����� �$��35��.+�� $��10,0,0,0q�$��Center=�.+��� -    ��Name$ 
GoBackPage��$��
Назад��$��80��$��16(�
��������� $��	10,0,10,0q�
���   �$��25��$��2�� J� Opacity$	 0.2Q��� -    ��Name$ FindDateChecker��$��0,4,5,0q��� $	��С:��$��0,0,10,0q�$��16(�(  "System.Windows.Controls.DatePicker  -     Name$ 	BeginDate��$��0,0,10,0q�$��12(�{�.  �� $
��по��$��0,0,10,0q�$��16(�  -   $ EndDate��$��12(�{�.  �� $��	10,0,10,0q�
���   �$��25��$��2��$	 0.2Q��� $��Найдено: ��$��16(��� -    ��Name$ FindCounterData��$	��0/0��$��16(�� $��0,0,10,0q�$��Right=�$��Center=�.+��� -   $ Print��$��Печать отчета��$��16(�$	��120��
��������� -    ��Name$ SessionList��$	��400����.  ����  �� 
TargetType" ��.���  �� Property � ��	 �� Value$	 Stretch=�
 �� View
 �� .P��� -	   SessionCodeGrid��.���� $��Код сессии��$��16(� �� CellTemplate �� .R��� $��Center=�$��16(����� *	idSession+�� -
   SessionInformationGrid��.���� $��Информация��$��16(� �� .R��� .+�� .+��� $��Дата сеанса:��$��16(��� $��16(����� *dateSession+� .+��� $��Время сеанса:��$��16(��� $��16(����� *timeSession+�� -   AnalyticsGrid��.���� $%��Аналитика продаж��$��16(� �� .R��� .+�� .+��� $=��7Количество проданных билетов:��$��16(��� $��16(����� *ticketCountSession+� .+��� $$��Суммарные сборы:��$��16(��� $��16(����� *ticketSummarySession+