   M S B A M L   `   `   `  ����D  @PrismDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PrismDemo.ViewsPrismDemo.Views  5          PrismDemo.Views.HomeView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:PrismDemo.Views     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5	      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5      .��5      ?  9MaterialDesignThemes.Wpf.Transitions.TransitioningContent  C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffects 5   
   ;  5MaterialDesignThemes.Wpf.Transitions.TransitionEffect  5         Kind$ SlideInFromBottom=�6"   .��5   
   �� $
��Auto=�6   .��5      �� .+�5      �  � RowDefinitions 5      �� 5      $
��Auto �6(   .+�5      (  "MaterialDesignThemes.Wpf.ColorZone  $��40q�5         CornerRadius$ 0��5         Mode$ PrimaryDark=�5      .��5       �� .+�5!      �� $��0,0,0,16q�5"   !   $��32(�5#   !   $
��Bold"�5$   !   ��5%   !   �� *WelcomeHeader+�� 5&      $	��600��5'   !   $��0,0,0,24q�5(   !   $
��Left=�5)   !   $��16(�5*   !   $
��Wrap=�5,   !   ��5+   !   �� *WelcomeSubheader+5,   !   �� 5-      $	��120��5.   !   $��了解更多��50   !    #  MaterialDesignRaisedLightButton#��[  51   !   ��5/   !   �� *LearnMoreCommand+51   !   52      53      54      � 57      $��40,30,40,40q�6    58      �� 59      $
��Auto �6(   �� 5:      $
��Auto �6(   �� 5;      $
��Auto �6(   .+�5?      �� $��0��5@      $��0,0,0,24q�5A      $��24(�5B      $��SemiBold"�5C      $��我们的优势��5D      �� 5F      $��1��6#   ��60   �� *
Features+��5G      �� 5H      .R�5I   "   � 5J      ��5L      �� 5M      .R�5N   "   #  MaterialDesignThemes.Wpf.Card  $	��280��5O   %   $��	0,0,24,24q�5P   %      UniformCornerRadius$ 8Q�5R   %   ��5Q   %   �� *Color+.��5S   &   � $��16q�6+    5T   *   �� 5U   .   $
��Auto �6<   �� 5V   .   $
��Auto �6<   �� 5W   .   $��* �6<   .+�5Z   *   '  !MaterialDesignThemes.Wpf.PackIcon  $��48��5[   -   $��48��5\   -   
�������   Kind 5^   -   �� *Icon+�� 5`   *   $��1��5a   -   $��0,16,0,8q�5b   -   $��18(�5c   -   $
��Bold"�5d   -   
���������5f   -   �� *Title+�� 5h   *   $��2��5i   -   $��80��5j   -   
�������$
��Wrap=�5m   -   ��5l   -   �� *Description+5m   -   5n   '   5o   #   5p      5r      �� 5t      $��2��6!    �� Orientation$ Vertical=�6.   .+�5v      � $��2��6   $��0,20,0,0q�6,    5w      �� 5x   "   $
��Auto �60   �� 5y   "   $
��Auto �60   .+�5|      �� $��0��5}   !   $��0,0,0,0q�5~   !   $��28(�5   !   $��SemiBold"�5�   !   $��最新动态��5�   !   �� 5�      $��1��6+   ��68   �� *
RecentNews+��5�   "   �� 5�   &   .R�5�   *     $��0,0,0,16q�62   .��5�   .   � $��16q�63   	 � ColumnDefinitions	 5�   2   �� 5�   6   $��* �6G   �� 5�   6   $
��Auto �6G   .+�5�   2   �� .+�5�   6   �� $��0,0,0,8q�5�   9   $��16(�5�   9   $
��Bold"�5�   9   ��5�   9   �� *Title+�� 5�   6   $��0,0,0,8q�6A   $ 
Horizontal=�6R   .+�5�   :     $��0,0,8,0q�5�   =   $��Center=�5�   =   $ Calendar=�5�   =   �� 5�   :   
 J� Opacity$	
 0.7Q�6D   ��6R   �� *Date+5�   7   �� 5�   6   $��60��5�   9   $��0,0,0,8q�5�   9   $��WordEllipsis=�5�   9   $
��Wrap=�5�   9   ��5�   9   �� *Description+5�   9   �� 5�   6   $
��Left=�5�   9   $��阅读更多��5�   9     MaterialDesignFlatButton#��[ 5�   9   ��5�   9   �� *DataContext.ReadNewsCommand+ �� RelativeSource ��  �� AncestorType ItemsControl��5�   9   �� 5�   9   5�   3   �� 5�   2   $��1��5�   5   $	��100��5�   5   $	��100��5�   5    �� CornerRadius$ 50��5�   5   ��5�   5   �� *Color+._�5�   6     $��50��5�   9   $��50��5�   9   $��Center=�5�   9   $��Center=�5�   9   
������� 5�   9   �� *Icon+5�   3   5�   /   5�   +   5�   '   5�      5�        5�      $��3��5�      $��0,10,0,0q�5�      ���#f5f5f5.��5�      � $��24q�6#   	 5�   "   �� 5�   &   $��* �67   �� 5�   &   $
��Auto �67   .+�5�   "   �� .+�5�   &   �� $��0,0,0,8q�5�   )   $��22(�5�   )   $
��Bold"�5�   )   $��准备好开始了吗？��5�   )   �� 5�   &   $��0,0,0,16q�5�   )   $u��o我们随时准备好为您提供支持和服务，联系我们了解更多信息或获取定制解决方案。��5�   )   $
��Wrap=�5�   )   �� 5�   &   $	��120��5�   )   $
��Left=�5�   )   $��联系我们��5�   )     MaterialDesignRaisedButton#��[ 5�   )   ��5�   )   �� *ContactUsCommand+5�   )   5�   #     5�   "   $��1��5�   %   $��80��5�   %   $��80��5�   %   $��24,0,0,0q�5�   %   $��Center=�5�   %     PrimaryHueDarkBrush#���  5�   %   $ Email=�5�   %   5�      5�      5�      5�      5�      5�      5�      5�      