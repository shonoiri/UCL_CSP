   M S B A M L   `   `   `  ����@  <EX_01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EX_01EX_01  5   	       EX_01.EditRoomWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:EX_01     5   	     � Title$  Meeting Room Edit/New��5   	   $	��500��6'   $	��500��64    � 	Resources 5	         TextBoxErrorTemplate �� 
TargetType �� Orientation �� TextAlignment � ToolTip �� ElementName �� Path ��Name��%�  &	        �� 5
   
   " ��67   .R�5      ]� .+�5      �� $ 
Horizontal=�6   $��Right=�66   $��Bottom=�6R   .+�5      � $��12��6   $��12��6&   .+�5      A� $��12��6"   $��12��6-   
���  ��$��Right=�5   "   $��Center=�5   "   5      �� 5      
�������$��Heavy"�5   $   $��8(�5   $   $��Right=�5   $   $��Center=�5   $   $ Center=�5   $    5   $   �� $ ErrorAdorner��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�X5      5      �� 5      
���  ��$��Heavy"�5       $��0,0,0,2q�5       ��5        �� $ ErrorAdorner��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�5"      5#      �� 5$      $ ErrorAdorner��6,   6E   5%      5&      5'      .��5(      � .+�5)   
   �� -   5        $��CurrentRoom��5)      $ Vertical=�6)   $
��Left=�6@   $��20 20 20 20q�6[   .+�5*      �� $��20(�6   $��Center=�6&   $��0 0 0 10q�6C   Meeting Room6d   �� 5+      Name6   �� 5,      $	��350��6   $
��Left=�6"   	 =� ErrorTemplate#	 [  6=   .��6~   ��5-      �� 5.      $
 Name�6   
 �� UpdateSourceTrigger$
 PropertyChanged=�6*    �� ValidationRules 5/         EX_01.DataValidator  50         DataType$
 Name��62   52      .��53       54      �� 55      Code6   �� 56      $	��350��6   $
��Left=�6"   #	 [  6=   .��6~   ��57      �� 58      $
 Code�6   $
 PropertyChanged=�6*    59        5:      $
 Code��62   5<      .��5=       5>      �� 5?      Meeting Center Code6-   �� 5@      $	��350��6   $
��Left=�6"    �� 
IsReadOnly . .��6O   ��5A      �� 5B      $ MeetingCenterCode�6   69   .��5C       5D      �� 5E      
Capacity6"   �� 5F      $	��350��6   $
��Left=�6"   #	 [  6=   .��6~   ��5G      �� 5H      $ Capacity�6   $
 PropertyChanged=�6.    5I        5J      $ Capacity��62   5L      .��5M       5N      �� 5O      Description6%   �� 5P      $	��350��6   $��50��6"   $
��Left=�6.    �� TextWrapping$
 Wrap=�5Q       �� AcceptsReturn . #	 [  5S      .��6W   ��5T      �� 5U      $ Description�6   $
 PropertyChanged=�61    5V        5W      $ Description��62   5Y      .��5Z       5[      �� 5\      $
��Left=�6>    d� 	IsChecked 6   �� *VideoConference+.��6Y   Video Conference6k   5]      �� 5^   
   $ 
Horizontal=�6   $��Right=�6.   $��Bottom=�6J   $��	0 0 20 20q�6e   .+�5_      �� -   5        $��BtnSave��5_      $��80��6$   $��Right=�6/   $��	0 20 10 0q�6K   .��6t   Save6z   �� 5`      -   5        $��	BtnCancel��5`      $��80��6&   $��0 20 0 0q�61   .��6[   Cancel6c   5a      5b      5c      