   M S B A M L   `   `   `  ����  	PrismDemo    PrismDemo.Views.NewsView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5      .��5      � $��20q�6    � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� $��公司新闻��6   $��24(�5      $��0,0,0,20q�5      �� 5      $��1��6    �� Orientation$ 
Horizontal=�6)   .+�5      �� $	��200��6   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 搜索新闻��5         
IsFloating$
 True��5       %  !MaterialDesignFloatingHintTextBox#��[  5      $��0,0,8,0q�5      ��5      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5         MaterialDesignRaisedButton#��[ 5!      ��5       �� *SearchCommand+.��5"      �� $ 
Horizontal=�6!   .+�5#      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Magnify=�6&   $��Center=�65   �� 5$      $��搜索��6$   $��4,0,0,0q�6.   5%      5&      5'      5(      �� 5*   
   $��1��6   $
��Auto=�6$   .��5+      �  5,      �� 5-      $��2* �6'   �� 5.      $��1* �6'   .+�52      �� $��0��6   $��0,0,16,0q�6T   ��6/   �� *FilteredNews+��53      �� 54      .R�55      #  MaterialDesignThemes.Wpf.Card  $��0,0,0,16q�6&   .��56   "   �  57   &   �� 58   *   $
��Auto �6;   �� 59   *   $��* �6;   .+�5<   &   �� $��0��6-   $	��120��5=   ,   $	��120��5>   ,   ���#e0e0e0$��0,0,16,0q�5@   ,   ._�5A   *     $��60��5B   5   $��60��5C   5   $��Center=�5E   5   $��Center=�5F   5    5A   6   �� *Icon+��5D   5   �� *	IconColor+5F   5   5G   '   �� 5I   &   $��1��61   $
��0,12q�6A   .+�5J   *   �� $��16(�5K   2   $
��Bold"�5L   2   ��5J   4   �� *Title+5L   2   �� 5N   *   $ 
Horizontal=�65   $��0,4,0,8q�6N   .+�5O   .     $ Calendar=�6:   $��Center=�5P   9   $��0,0,4,0q�5Q   9   �� 5R   .    J� Opacity$	 0.7Q�5S   6   ��5R   8   �� *PublishDate+5S   6     5U   .   $	 Tag=�6:   $��Center=�5V   9   $��16,0,4,0q�5W   9   �� 5X   .   $	 0.7Q�5Y   6   ��5X   8   �� *
Category+5Y   6   5Z   +   �� 5\   *   $
��Wrap=�5]   2   $��60��5^   2   $��WordEllipsis=�5_   2   ��5\   4   �� *	Summary+5_   2   �� 5a   *   $��阅读全文��61     MaterialDesignFlatButton#��[ 5e   0   $
��Left=�5f   0   $��0,8,0,0q�5g   0   ��5b   0   �� *DataContext.ReadNewsCommand+	 �� RelativeSource	 �� 
 �� AncestorType
 ItemsControl��5d   0   �� 5g   0   5h   '   5i   #   5j      5k      5m      �� 5p      $��1��6   .+�5r        $��0,0,0,16q�6   .��5s      �� $��16q�6%   .+�5t      �� $��新闻分类��6(   $��18(�5u   &   $
��Bold"�5v   &   $��0,0,0,12q�5w   &   �� 5y      ��6&   �� *
Categories+ �� SelectedItem 5z   $   �� *SelectedCategory+��5{   "   �� 5|   &   .R�5}   *   ��  �� Padding$ 4q�6E   ��64   �� 6E   5~   '   5�      5�      5�        5�      .��5�      �� $��16q�6%   .+�5�      �� $��热门新闻��6(   $��18(�5�   &   $
��Bold"�5�   &   $��0,0,0,12q�5�   &   �� 5�      ��6+   �� *	HotNews+��5�   "   �� 5�   &   .R�5�   *   �� $��0,0,0,12q�65   .+�5�   .   �� $��Medium"�5�   6   $
��Wrap=�5�   6   ��5�   8   �� *Title+5�   6   �� 5�   .   $	 0.7Q�5�   6   $��0,4,0,0q�5�   6   ��5�   8   �� *PublishDate+5�   6   �� 5�   .   $��查看��65   #��[ 5�   4   $
��Left=�5�   4   $	��4,0q�5�   4   ��5�   4   �� *DataContext.ReadNewsCommand+	 �� 
 ItemsControl��5�   4   �� 5�   4   �� 5�   .   $��0,8,0,0q�68   5�   +   5�   '   5�      5�      5�      5�      5�      5�      5�      5�      