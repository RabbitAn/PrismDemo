   M S B A M L   `   `   `  ����  	PrismDemo(    "PrismDemo.Views.EventPublisherView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5      .��5      � $��20q�6   .+�5	   
   #  MaterialDesignThemes.Wpf.Card  $��8q�6      UniformCornerRadius$ 8Q�6   .��5
      �� $��24q�6   .+�5      �� $��事件发布��6   $��24(�5      $
��Bold"�5      $��0,0,0,24q�5      �� 5      $����事件聚合器（Event Aggregator）是Prism框架中用于组件间松耦合通信的功能。它基于发布-订阅模式，一个组件发布事件，其他感兴趣的组件可以订阅这些事件。��6   $
��Wrap=�5      $��0,0,0,24q�5        5      $��0,0,0,16q�6   $ 8Q�6,   ���#E3F2FD.��5      �� $��16q�6!   .+�5      �� $��发送新消息��6$   $��18(�5   "   $��SemiBold"�5   "   $��0,0,0,16q�5   "   �� 5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 您的名字��6"      
IsFloating$
 True��5        %  !MaterialDesignFloatingHintTextBox#��[  5       $��0,0,0,16q�5       ��5       �� *
SenderName+ �� UpdateSourceTrigger$ PropertyChanged=� �� NotifyOnSourceUpdated$
 True��5       �� 5!      $ 消息内容��6"   $
 True��5"       #��[  5$        �� TextWrapping$
 Wrap=�5%       $��80��5&        �� AcceptsReturn$
 True��5'       " �� VerticalScrollBarVisibility$
 Auto=�5(       $��0,0,0,16q�5)       	 � Cursor$	  ��62   ��5#       �� *MessageText+$ PropertyChanged=�$
 True��5)   2   �� 5+      
 �� Orientation$
 
Horizontal=�6%   $��Right=�5,   #   .+�5-      �� $��清除��6%      MaterialDesignOutlinedButton#��[ 5/   $   $��0,0,16,0q�50   $   ��5.   $   �� *ClearMessageCommand+50   $   �� 51      $��发送消息��6%     MaterialDesignRaisedButton#��[ 53   $   ��52   $   �� *SendMessageCommand+53   $   54      55      56      �� 58      $��0,8,0,16q�6   �� 5:      $��已发送的消息��6   $��18(�5;      $��SemiBold"�5<      $��0,0,0,16q�5=      �� 5?      $	��300��5@      $��0q�5A      ��5?      �� *SentMessages+��5B      �� 5C      .R�5D        $	��0,4q�6&   $ 4Q�63   .��5E   "   � $��16,12q�6'    � RowDefinitions 5F   &   �� 5G   *   $
��Auto �68   �� 5H   *   $
��Auto �68   �� 5I   *   $
��Auto �68    � ColumnDefinitions 5K   &   �� 5L   *   $��* �6;   �� 5M   *   $
��Auto �6;   .+�5P   &   �� $��0��60   $
��Bold"�5R   .   ��5Q   .   �� *Sender+5R   .   �� 5T   &   $��1��60   $
��Wrap=�5V   .   $��0,4,0,4q�5W   .   ��5U   .   �� *	Message+5W   .   �� 5Y   &   $��2��60   $��Italic$�5[   .   $��11(�5\   .    J� Opacity$	 0.7Q�5]   .   ��5Z   .   �� *	Timestamp+5]   .   '  !MaterialDesignThemes.Wpf.PackIcon  5_   &   $��3��62   $��1��5`   0      Kind$
 Mace=�5a   0   $��24��5b   0   $��24��5c   0   $��Center=�5d   0   $��16,0,0,0q�5e   0     PrimaryHueDarkBrush#���  5f   0   5g   #   5h      5i      5k      �� 5m      $l��f注意：发送的消息也会显示在「事件订阅」页面，演示了跨视图的事件通信。��6   $
��Wrap=�5n      $��0,16,0,0q�5o      $	 0.7Q�5p      �� 5r      $��查看订阅者页面��6     MaterialDesignFlatButton#��[ 5t      $
��Left=�5u      $��0,8,0,0q�5v      ��5s      �� *GoToSubscriberCommand+5v      5w      5x      5y      5z      