   M S B A M L   `   `   `  ����  	PrismDemo     PrismDemo.Views.MainWindow   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T	 PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/  	 5      $
��1280��5	      $	��720��5
        � Title  5      �� *Title+'  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      CaptionHeight$ 10Q�5         CornerRadius$ 10��5         GlassFrameThickness$ 0q�5         UseAeroCaptionButtons$ False��5      .��5      )  #MaterialDesignThemes.Wpf.DialogHost  
���   �   DialogTheme$ Inherit=�5   	      
Identifier$ 
RootDialog��5   	   .��5   
   )  #MaterialDesignThemes.Wpf.DrawerHost  $��0��6      IsLeftDrawerOpen 6%   �� 	 �� ElementName$	 MenuToggleButton��
 �� Path$
 	IsChecked�.��5      ?  9MaterialDesignThemes.Wpf.Transitions.TransitioningContent  C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffects 5      ;  5MaterialDesignThemes.Wpf.Transitions.TransitionEffect  5         Kind$ FadeIn=�6*     5      $ SlideInFromBottom=�6*   .��5      �  � RowDefinitions 5      �� 5      $
��Auto �6(   �� 5      $��* �6(   .+�5!      (  "MaterialDesignThemes.Wpf.ColorZone  $��0��5"      $��16q�5#         Mode$ Inverted=�5$      .��5%      ]� .+�5&      �� -   5        $��25��5'   !   $��25��5(   !   $	��5,0q�5)   !   
���  ��
������ $��Right=�5-   !    /  +MaterialDesignFloatingActionMiniLightButton#��[  5.   !   .��5/   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�6.   50      �� 51      -   5        $��25��52   !   $��25��53   !   $	��5,0q�54   !   
��� ���
������ $��Right=�58   !   #��[  59   !   .��5:   "     $ CheckboxBlankOutline=�6.   5;      �� 5=      -   5        $��25��5>   !   $��25��5?   !   $	��5,0q�5@   !   
���2�2�
������ $��Right=�5D   !   #��[  5E   !   .��5F   "     $ 
MinusThick=�6.   5G      �� 5I       �� Orientation$ 
Horizontal=�6)   .+�5J   "   d� -   5         d�Name$ MenuToggleButton��5K   %      OpenDrawerCommand#��Z 5L   %   "	  System.Windows.Controls.Dock 	  Left#��Z 5M   %    d� 	IsChecked$ FalseY�5N   %    ' #MaterialDesignHamburgerToggleButton#��[ 5O   %   5P      5Q      5S      �� 5U      $��1��6%   !
 	 Prism.Regions.RegionManager 
  
RegionName$ 
MainRegion��62   5V      5W         LeftDrawerContent 5Z      ]� 5[      $	��220��6   $��0,0,0,0q�6+   .+�5\      d� $��16q�5]      $��Right=�5^      $	��Top=�5_      #��[ 5a       5`      �� $	 MenuToggleButton��$
 	IsChecked� �� Mode$ TwoWay=�5a      �� 5b      -   5        $��NavigationListBox��5c      ��5d      �� *NavigationItems+��5f      �� 5g      .R�5h   "   �� $ Vertical=�6-   5i      ��5k      �� 5l       �� 
TargetType" ��6$   .�5m   "   ��  �� Property � �� �� Value 5n   &   �� 5o   *    �� 
TargetType" ��6:   .R�5p   .   �� $��
mainBorder��65   
������ ._�5q   2   � $��35��67   .+�5r   6   �� $��
headBorder��6=   �� 5s   6   $��contenterBorder��6=   �� 5t   6   $
��20,0q�5u   9   $��Center=�5v   9   $ 
Horizontal=�5w   9   .+�5x   :     $��Center=�5y   =   $��22(�5z   =    5{   =   �� *Icon+�� 5|   :   $��10q�5}   =   $��Center=�5~   =   ��5   =   �� *Name+5�   7   5�   3   5�   /    �� Triggers 5�   .   P� 5�   2    P� 
SourceName$ 
mainBorder��6:    P� Property �@J�IsMouseOver P� Value$
 True��6i   . �5�   6   ��   �� 
TargetName$  
headBorder��6=    � ��$  3 0 0 0q�6w   �� 5�   6   $  
headBorder��6=    � �� �#1581ff6s   �� 5�   6   $  contenterBorder��6=    � �� �#1581ff6w   �� 5�   6   $  contenterBorder��6=    �@J�Opacity$
  0.1Q�6t   5�   3   P� 5�   2    �@��
IsSelected$
 True��6P   . �5�   6   �� $  
headBorder��6=    � ��$  3 0 0 0q�6w   �� 5�   6   $  
headBorder��6=    � �� �#1581ff6s   �� 5�   6   $  contenterBorder��6=    � �� �#1581ff6w   �� 5�   6   $  contenterBorder��6=    �@J�Opacity$
  0.1Q�6t   5�   3   5�   +   5�   #   5�      *  $Microsoft.Xaml.Behaviors.Interaction!   Triggers! 5�      +  %Microsoft.Xaml.Behaviors.EventTrigger  5�      "   	EventName$" SelectionChanged��6-   *  $Microsoft.Xaml.Behaviors.TriggerBase#   Actions.# 5�   "   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  $   Command$ 68   �� *SelectionChangedCommand+%   CommandParameter% 6d   �� & �� RelativeSource& �� ' �� AncestorType' ListBox$
 SelectedItem�5�      5�      5�      5�      5�      5�      