   M S B A M L   `   `   `  ����@  <EX_01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EX_01EX_01  5   	       EX_01.EditCenterWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:EX_01     5   	     � Title$  EditCenterWindow��5   	   $	��500��6"   $	��500��6/    � 	Resources 5	         TextBoxErrorTemplate �� 
TargetType �� Orientation �� TextAlignment � ToolTip �� ElementName �� Path ��Name��%�  &	        �� 5
   
   " ��67   .R�5      ]� .+�5      �� $ 
Horizontal=�6   $��Right=�66   $��Bottom=�6R   .+�5      � $��12��6   $��12��6&   .+�5      A� $��12��6"   $��12��6-   
���  ��$��Right=�5   "   $��Center=�5   "   5      �� 5      
�������$��Heavy"�5   $   $��8(�5   $   $��Right=�5   $   $��Center=�5   $   $ Center=�5   $    5   $   �� $ ErrorAdorner��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�X5      5      �� 5      
���  ��$��Heavy"�5       $��0,0,0,2q�5       ��5        �� $ ErrorAdorner��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�5"      5#      �� 5$      $ ErrorAdorner��6,   6E   5%      5&      5'      .��5(      � .+�5*   
   �� -   5        $��Centre��5*      $ Vertical=�6#   $
��Left=�6:   $��20 20 20 20q�6U   .+�5+      �� $��Center=�6   $��20(�65   $��0 0 0 10q�6C   Meeting Centre6f   �� 5,      Name6   �� 5-      $	��350��6   $
��Left=�6"   	 =� ErrorTemplate#	 [  6?   .��6�   ��5.      �� 5/      $
 Name�6   
 �� UpdateSourceTrigger$
 PropertyChanged=�6*    �� ValidationRules 50         EX_01.DataValidator  51         DataType$
 Name��62   53      .��54       55      �� 56      Code6   �� 57      $	��350��6   $
��Left=�6"   #	 [  6=   .��6~   ��58      �� 59      $
 Code�6   $
 PropertyChanged=�6*    5:        5;      $
 Code��62   5=      .��5>       5?      �� 5@      Description6%   �� 5A      $	��350��6   $��50��6"   $
��Left=�6.    �� TextWrapping$
 Wrap=�6I    �� AcceptsReturn . #	 [  6r   .��6�   ��5B      �� 5C      $ Description�6   $
 PropertyChanged=�61    5D        5E      $ Description��62   5G      .��5H       5I      5J      �� 5K   
   $ 
Horizontal=�6   $��Right=�6.   $��Bottom=�6J   $��	0 0 20 20q�6e   .+�5L      �� -   5        $��BtnSave��5L      $��80��6$   $��Right=�6/   $��	0 20 10 0q�6K   .��6t   Save6z   �� 5M      -   5        $��	BtnCancel��5M      $��80��6&   $��0 20 0 0q�61   .��6\   Cancel6d   5N      5P      5Q      