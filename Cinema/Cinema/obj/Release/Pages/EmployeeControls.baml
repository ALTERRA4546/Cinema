   M S B A M L   `   `   `  ���� A  =Cinema, Version=0.7.1.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CinemaCinema      Cinema.EmployeeControls   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     &localclr-namespace:Cinema     -     N� Title$  EmployeeControls��.��� .+��� .+�� 
����� �$��35��.+�� $��10,0,0,0q�$��Center=�.+��� -    ��Name$ AddEmployee��$��Добавить��$
��Left=�$	��120��$��16(�
��������� -   $ RemoveEmployee��$��Удалить��$
��Left=�$	��120��$��16(�
��������� $��	10,0,10,0q�
���   �$��25��$��2�� J� Opacity$	 0.2Q��� $��Поиск:��$��0,0,10,0q�$��16(��� -    ��Name$ FindData��$��0,0,10,0q�$	��200��$��16(��� $��	10,0,10,0q�
���   �$��25��$��2��$	 0.2Q��� $��Найдено: ��$��16(��� -    ��Name$ FindCounterData��$	��0/0��$��16(��� -    ��Name$ EmployeeList��$	��400����.  ����  �� 
TargetType" ��.���  �� Property � �� �� Value$ Stretch=�	 �� View	 �� .P��� -   EmployeeCodeGrid��.���� $!��Код сотрудника��$��16(�
 �� CellTemplate
 �� .R��� $��Center=�$��16(����� *
employeeID+�� -   	ImageGrid��.���� $��Фотография��$��16(�
 �� .R��� $	��400��$��Center=����� *employeePhoto+�� -	   EmployeeInfoGrid��.���� $2��,Информация о сотруднике��$��16(�
 �� .R��� .+�� .+��� $��16(����� *employeeSurname+�� $��16(����� *employeeName+�� $��16(����� *employeePatronymic+� .+��� $��Телефон:��$��16(��� $��16(����� *employeePhone+� .+��� $��Почта:��$��16(��� $��16(����� *employeeEmail+� .+��� $��Должность:��$��16(��� $��16(����� *employeeRole+�� -
   AuthorizationGrid��.���� $)��#Данные авторизации��$��16(�
 �� .R��� .+�� .+��� $��Логин:��$��16(��� $��16(����� *employeeLogin+� .+��� $��Пароль:��$��16(��� $��16(����� *employeePassword+