   M S B A M L   `   `   `  ���� A  =Cinema, Version=0.7.1.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CinemaCinema      Cinema.AddOrEditMovie   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:Cinema     -     � Title$  
Фильм��$	��620��$
��1170�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.���    MainColorStyle#���   .+��� .+�� .+���   SecondColorRectangleStyle#���  �� $��
ФИЛЬМ��  TitleTextBlockStyle#���  � $��Center=�$��10q�.+��� .+�� $��0,0,0,10q�.+��� $��Название:��  MainLableStyle#���  �� -    ��Name$ 
MovieTitle��$
��Left=�$��	200,0,0,0q�$	��350��  MainTextBoxStyle#���  � $��0,0,0,10q�.+��� $,��&Страна производства:��#���  �� -    ��Name$ MovieCountry��$
��Left=�$��	200,0,0,0q�$	��350��  MainComboBoxStyle#���    MainSelectedComboBoxStyle#���  � $��0,0,0,10q�.+��� $"��Год публикации:��#���  �� -   $ MovieYearOfPublication��$
��Left=�$��	200,0,0,0q�$	��350��#���  � $��0,0,0,10q�.+��� $&�� Хронометраж (мин):��#���  �� -   $ MovieTiming��$
��Left=�$��	200,0,0,0q�$	��350��#���  � $��0,0,0,10q�.+��� $*��$Возрастной рейтинг:��#���  �� -   $ MovieAgeRating��$
��Left=�$��	200,0,0,0q�$	��350��#���  � $��0,0,0,10q�.+��� $��Описание:��#���  �� -   $ MovieDescription��$
��Left=� �� TextWrapping$
 Wrap=�$��	200,0,0,0q�$	��350��$	��200��$	��200��#���  � .+��� .+�� .+��� $��Обложка:��#���  � $��	270,0,0,5q�$
��Left=�.+��� -    ��Name$ 
MovieCover��$	��200��$	��200��$��Center=�$y�	Collapsed=��� -	    ��Name$ 	LoadCover��$��Загрузить��$��	270,0,0,0q�$	��200��$
��Left=�  MainButtonStyle#���  �� $��558,0,560,0q�
���   �$��2�� J� Opacity$	 0.2Q��� $��	580,0,0,0q�.+�� $��0,0,0,10q�.+��� .+�� $��0,0,0,10q�.+��� $��Жанры:��#���  � .+��� -
   $ MovieGenere��$
��Left=�$
 Wrap=�$��80,0,0,0q�$	��455��#���  	 �� 
IsReadOnly	 . �� -   $ 
ClearGenre��$��X��$��31.28��  MainClearButtonStyle#���  &   System.Windows.Controls.DataGrid  -   
  Name$
 MovieGenreGrid��$	��120��$��0,0,0,10q�   AutoGenerateColumns .     
IsReadOnly .  	 MainDataGridStyle#��� 	    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Код жанра��   MaxWidth$ 0Q�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	idGenre+  $ Жанр�� �� *
genreTitle+� $��Center=�.+��� -   $ AddGenre��$��0,0,5,0q�$��Добавить��$	��160��#���  �� -   $ 	EditGenre��$��0,0,5,0q�$ ��Редактировать��$	��160��#���  �� -   $ RemoveGenre��$��Удалить��$	��160��#���  � .+��� .+�� $��0,0,0,10q�.+��� $��Актеры:��#���  � .+��� -   $ 
MovieActor��$
��Left=�$
 Wrap=�$��80,0,0,0q�$	��455��#���  	 . �� -   $ 
ClearActor��$��X��$��31.28��#���    -   $
 MovieActorGrid��$	��120��$��0,0,0,10q�   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ *  .   . 
�������   $ Код актера��$ 0Q� �� *	idActor+  $ Фамилия�� �� *actorSurename+  $ Имя�� �� *	actorName+  $ Отчество�� �� *actorPatronymic+  $ Прозвище�� �� *actorNickname+� $��Center=�.+��� -   $ AddActor��$��0,0,5,0q�$��Добавить��$	��160��#���  �� -   $ 	EditActor��$��0,0,5,0q�$ ��Редактировать��$	��160��#���  �� -   $ RemoveActor��$��Удалить��$	��160��#���  �� -   $
 Save��$��0,10,0,0q�$��Сохранить��$	��250��#���  