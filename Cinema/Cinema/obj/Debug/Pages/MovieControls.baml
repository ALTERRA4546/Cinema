   M S B A M L   `   `   `  ����A  =Cinema, Version=0.7.1.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CinemaCinema  5          Cinema.MovieControls   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:Cinema     5      -   5          N� Title$  MovieControls��5	      .��5      � 
�������.+�5   
   �� .+�5      � 
����� �$��35��6%   .+�5      � $��10,0,0,0q�6   $��Center=�6.   .+�5      �� -   5         ��Name$ AddMovie��5      $��Добавить��6/   $
��Left=�6B   $	��120��6]   $��16(�6i   
�������6�   �� 5      -   5        $ RemoveMovie��5      $��Удалить��62   $
��Left=�6D   $	��120��6_   $��16(�6k   
�������6�   �� 5      $��	10,0,10,0q�6    
���   �$��25��6@   $��2��6L    J� Opacity$	 0.2Q�6V   �� 5      $��Поиск:��6    $��0,0,10,0q�6.   $��16(�6@   �� 5      -   5         ��Name$ FindData��5      $	��200��60   $��16(�6<   6J   �� 5      $��	10,0,10,0q�6    
���   �$��25��6@   $��2��6L   $	 0.2Q�6V   �� 5      $��Найдено: ��6    $��16(�61   �� 5      -   5         ��Name$ FindCounterData��5       $	��0/0��69   $��16(�6D   5      5      �� 5      -   5         ��Name$ 	MovieList��5      $	��400��6*   ��.  ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value$ Stretch=�6G   5      	 �� View	 5       �� 5!      .P�5"      �� -   5        MovieCodeGrid��.��5#      �� $��Код фильма��6$   $��16(�69   
 �� CellTemplate
 5$      �� 5%   "   .R�5&   &   �� $��Center=�6H   $��16(�6f   ��6,   �� *	movieID+6f   5'   #   5)      �� 5*      -   5        	ImageGrid��.��5+      �� $��Постер��6$   $��16(�65   
 5,      �� 5-   "   .R�5.   &   �� $	��400��6J   $��Center=�6Z   ��6,   �� *
movieCover+6Z   5/   #   51      �� 52      -	   5        MovieInfoGrid��.��53      �� $*��$Информация о фильме��6$   $��16(�6B   
 54      �� 55   "   .R�56   &   �� .+�57   *   �� $��16(�6O   ��60   �� *
movieTitle+6O   � 58   *   .+�59   .   �� $��Жанр(ы):��64   $��16(�6G   �� 5:   .   $��16(�6S   ��64   �� *
movieGenre+6S   5;   +   � 5<   *   .+�5=   .   �� $"��Год публикации:��64   $��16(�6N   �� 5>   .   $��16(�6_   ��64   �� *movieYearOfPublication+6_   5?   +   � 5@   *   .+�5A   .   �� $��Хронометраж:��64   $��16(�6K   �� 5B   .   $��16(�6T   ��64   �� *movieTiming+6T   5C   +   � 5D   *   .+�5E   .   �� $*��$Возрастной рейтинг:��64   $��16(�6R   �� 5F   .   $��16(�6W   ��64   �� *movieAgeRating+6W   5G   +   � 5H   *   .+�5I   .   �� $��Страна:��64   $��16(�6F   �� 5J   .   $��16(�6U   ��64   �� *movieCountry+6U   5K   +   � 5L   *   .+�5M   .   �� $��Актеры:��64   $��16(�6F   �� 5N   .   $��5,0,0,0q�6U   $��Center=�6f   $
��Wrap=�6�   
���   �$��16(�6�   ��68   �� *movieActors+6�   5O   +   5P   '   5Q   #   5S      �� 5T      -
   5        DescriptionGrid��.��5U      �� $��Описание��6$   $��16(�67   
 5V      �� 5W   "   .R�5X   &   �� $
��Wrap=�6R   
���   �$��14(�6y   ��60   �� *movieDescription+6y   5Y   #   5[      5\      5^      5_      5`      5a      