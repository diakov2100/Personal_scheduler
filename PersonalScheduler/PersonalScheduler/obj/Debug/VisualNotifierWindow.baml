   M S B A M L   `   `   `  ����L  HPersonalScheduler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PersonalSchedulerPersonalScheduler  5   	   ,    &PersonalScheduler.VisualNotifierWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PersonalScheduler     5   	     � Title$  Notification��5   	   $	��180��6   $��265.333��6+    � 
ResizeMode$ NoResize=�6;    � Icon$ Icons/notification.png��6Q   .��5	      � .+�5
   
   �� -   5         ��Name$ EventDescription��5
      $��10,0,4,4q�6,    �� 
IsReadOnly . $��52��6P   $��Bottom=�6\   �� 5   
   -   5         ��Name$ image��5      $��Icons/notification.png��6   $��75��6?   $	��Top=�6K   $��4,4,0,0q�6c   $
��Left=�6t   $��77��6�   �� 5   
   -   5         ��Name$ 	NameLabel��5      $
��Name��6#   $��	90,10,0,0q�62   $��26��6E   $	��Top=�6Q   $
��Left=�6i   $��42��6�   �� 5   
   -   5        $ 
PlaceLabel��5      $��Place��6$   $��	90,35,0,0q�64   $��26��6G   $	��Top=�6S   $
��Left=�6k   $��42��6�   �� 5   
   -   5        $ 	EventName��5      $��
135,10,4,0q�6%    �� TextWrapping$
 Wrap=�69   $��Center=�6M    . $��25��6�   $	��Top=�6�   �� 5   
   -   5        $ 
EventPlace��5      $��
135,35,4,0q�6&   $
 Wrap=�6:   $��Center=�6N    . $��26��6�   $	��Top=�6�   �� 5   
   -   5        $ Description��5      $��Description��6%   $��
87,0,87,47q�6;   $��Center=�6O   $��34��6s   $��Bottom=�6   5      5      