   M S B A M L   `   `   `  ����H  DMessageSender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null`@clr-namespace:MessageSender.UsersControls;assembly=MessageSenderMessageSender.UsersControls  5   	    
ChatClient   ChatClient.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullEmatrialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   Tuserscontrols@clr-namespace:MessageSender.UsersControls;assembly=MessageSender  5   	     � Title$  
MainWindow��5	   	   $	��600��6   $	��600��6)    � WindowStyle$
 None=�65    � 
ResizeMode$ NoResize=�6H    � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $e _pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml?�6%   �� 5      $h bpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml?�6%   �� 5      $t npack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Blue.xaml?�6%   �� 5      $u opack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Indigo.xaml?�6%   %    5      .��5      �� ���	#FF212630$��2q�6%   ._�5   
   �  � RowDefinitions 5      �� 5      $
��0.6* �6    �� 5      $
��1.4* �6    �� 5      $��3* �6    �� 5      $��1* �6    �� 5      $
��0.3* �6    .+�5      � -   5         �Name$ 	gridTitle��5      $��0��6&   ���	#FF212630 � ColumnDefinitions 5      �� 5      �� 5       .+�5#      �� $��5,0,0,0q�6   $
��Left=�6/   $��Center=�5$      .+�5%      �� $��Chat Client��6   $��Center=�5&      $��Center=�5'      
�������$��40(�5)      $��Verdana)�5*      5+      �� 5-      $��1��6   	 �� Orientation$	 
Horizontal=�6-   $��Right=�6F   $
��20,0q�6b   .+�5.      �� -   5        
 ��Name$
 btnMinimize��5.       0  ,MaterialDesignFloatingActionMiniAccentButton#��[  62   $��50��6x   $��50��6�   
���������6�   Z� ��6�   Z� .��5/      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ WindowMinimize=�61   $��50��6G   $��50��6S   50      �� 52      -   5        $
 btnClose��52      #��[  6/   $��50��6u   $��50��6�   
���������6�   Z� ��6�   Z� .��53        $ Power=�61   $��50��6>   $��50��6J   54      55      56      � 57      $��1��6    58      �� 59      $
��0.6* �6$   �� 5:       5<      �� 5=      $��2* �6'   �� 5>      �� 5?      .+�5B      �� $��0��6   $��0��5C      $��
IP Address��5D      $��29(�5E      $��Center=�5F      $��Center=�5G      6   0MessageSender.UsersControls.IPAddressUserControl  5H      -   5          Name$ ucIpAddress��5H   5   $��1��6J   $��20(�6W   $��3q�6e   �� 5I      $��0��6   $��1��5J      $��Порт��5K      $��29(�5L      $��Center=�5M      $��Center=�5N      �� 5O      -   5         ��Name$ tbxPort��5O      $��1��5P      $��1��5Q      $��Center=�5R       �� TextAlignment$ Center=�5S       �� 	MaxLength$ 5��5T      $��20(�5U      $��4q�5V      $��
10,-13,0,0q�5W       * &MaterialDesignOutlinedTextFieldTextBox#��[ 5X      �� 5Y      -   5        $
 
btnConnect��5Y      $��60��5Z      $��60��5[      $��2��5\      $��1��5]      $��2��5^        MaterialDesignFlatButton#��[ 5_      $��0,0,5,0q�5`      $��0,0,0,0q�5a      ���	#FF212630
�������$��Подключиться��5d      ���	#FF212630$��Center=�5f      5g      5h      �� 5i      $��2��6   $��5q�5j      ���	#FF212630$��1q�5l       �� CornerRadius$ 5��5m      ._�5o      �� $
��Auto=�6   .��5p      �� -   5         ��Name$ tbkMainChatText��5p       $��15(�6:   $��WrapWithOverflow=�6H   $
��Auto=�6h   ��. 6�   5q      5r      � 5s      $��3��6    5t      �� 5u      $��2* �6'   �� 5v      �� 5w      .+�5z      �� -   5        $ 
tbxMessage��5z      $��20(�5{       �� TextWrapping$
 Wrap=�5|      $��Center=�5}      $��	0,-20,0,0q�5~      #��[ 5      $
��Auto=�5�      $��3q�5�      �� 5�      -	   5        $��btnSend��5�      $��1��5�      ���	#FF212630���	#FF212630$��50��5�      $��50��5�      $��Отправить��5�      $��Center=�5�      $��3q�5�      5�      �� 5�      -
   5        $��	btnGetLog��5�      ���	#FF212630���	#FF212630$��50��5�      $��50��5�      $��2��5�      $��Получить лог��5�      $��Center=�5�      $��3q�5�      5�      5�      5�      5�      5�      