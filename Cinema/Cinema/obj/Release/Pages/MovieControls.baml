   M S B A M L   `   `   `  ���� A  =Cinema, Version=0.7.1.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CinemaCinema      Cinema.MovieControls   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     &localclr-namespace:Cinema     -     N� Title$  MovieControls��.��� 
�������.+��� .+�� 
����� �$��35��.+�� $��10,0,0,0q�$��Center=�.+��� -    ��Name$ AddMovie��$��Добавить��$
��Left=�$	��120��$��16(�
��������� -   $ RemoveMovie��$��Удалить��$
��Left=�$	��120��$��16(�
��������� $��	10,0,10,0q�
���   �$��25��$��2�� J� Opacity$	 0.2Q��� $��Поиск:��$��0,0,10,0q�$��16(��� -    ��Name$ FindData��$	��200��$��16(��� $��	10,0,10,0q�
���   �$��25��$��2��$	 0.2Q��� $��Найдено: ��$��16(��� -    ��Name$ FindCounterData��$	��0/0��$��16(��� -    ��Name$ 	MovieList��$	��400����.  ����  �� 
TargetType" ��.���  �� Property � �� �� Value$ Stretch=�	 �� View	 �� .P��� -   MovieCodeGrid��.���� $��Код фильма��$��16(�
 �� CellTemplate
 �� .R��� $��Center=�$��16(����� *	movieID+�� -   	ImageGrid��.���� $��Постер��$��16(�
 �� .R��� $	��400��$��Center=����� *
movieCover+�� -	   MovieInfoGrid��.���� $*��$Информация о фильме��$��16(�
 �� .R��� .+��� $��16(����� *
movieTitle+� .+��� $��Жанр(ы):��$��16(��� $��16(����� *
movieGenre+� .+��� $"��Год публикации:��$��16(��� $��16(����� *movieYearOfPublication+� .+��� $��Хронометраж:��$��16(��� $��16(����� *movieTiming+� .+��� $*��$Возрастной рейтинг:��$��16(��� $��16(����� *movieAgeRating+� .+��� $��Страна:��$��16(��� $��16(����� *movieCountry+� .+��� $��Актеры:��$��16(��� $��5,0,0,0q�$��Center=�$
��Wrap=�
���   �$��16(����� *movieActors+�� -
   DescriptionGrid��.���� $��Описание��$��16(�
 �� .R��� $
��Wrap=�
���   �$��14(����� *movieDescription+