   M S B A M L   `   `   `  ����J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfApplication1.ViewWpfApplication1.View  5      %    WpfApplication1.View.CreateUser   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:WpfApplication1.View     5        N� Title$  
CreateUser��5	      .��5
      � .+�5   
   ��  �� Orientation$ Vertical=�6   .+�5      �� $ 
Horizontal=�6   .+�5      �� -   5         ��Name$ textFistname��5      $
��Left=�62   $��30��6M   $��0,0,0,0q�6Y   $
��Wrap=�6j   $��Prénom��6~   $	��100��6�   �� 5      -   5         ��Name$ textBoxFirstname��5      $
��Left=�64   $��20��6O   $��0,0,0,0q�6[    �� TextWrapping$
 Wrap=�6l   $�� ��6�   $	��150��6�   5      �� 5      $ 
Horizontal=�6   .+�5      �� -   5        $ textLastname��5      $
��Left=�62   $��30��6M   $��0,0,0,0q�6Y   $
��Wrap=�6j   $	��Nom��6~   $	��100��6�   �� 5      -   5        $ textBoxLastname��5      $
��Left=�63   $��20��6N   $��0,0,0,0q�6Z   $
 Wrap=�6k   $�� ��6   $	��150��6�   5      �� 5      $ 
Horizontal=�6   .+�5      �� -   5        $ 	textLogin��5      $
��Left=�6/   $��30��6J   $��0,0,0,0q�6V   $
��Wrap=�6g   $��Login��6{   $	��100��6�   �� 5      -   5        $ textBoxLogin��5      $
��Left=�60   $��20��6K   $��0,0,0,0q�6W   $
 Wrap=�6h   $�� ��6|   $	��150��6�   5      �� 5      $ 
Horizontal=�6   .+�5      �� -   5        $ textMdp��5      $
��Left=�6-   $��30��6H   $��0,0,0,0q�6T   $
��Wrap=�6e   $��Password��6y   $	��Top=�6�   $	��100��6�   �� 5      -   5        $ 
textBoxMdp��5      $
��Left=�6.   $��20��6I   $��0,0,0,0q�6U   $
 Wrap=�6f   $�� ��6z   $	��150��6�   5      �� 5      $ 
Horizontal=�6   .+�5      �� -	   5        $ txtRoles��5      $
��Left=�6.   $��30��6I   $
��Wrap=�6U   $��Rôles��6i   $	��100��6v   �� 5      -
   5         ��Name$ comboBox��5      $	��150��6-   $��20��69   $
��Auto=�5      $
��Auto=�5       ��. ��5"      �� 5#      .R�5$      �� ��6'   �� *	Role.Name+ d� 	IsChecked 6E   ��  �� Path$ 	IsChecked� �� Mode$ TwoWay=�
	 � Tag	 5%      �� *FindAncestor+
 �� AncestorType"
 ��5&      5(      5)      �� 5*      $ 
Horizontal=�6   .+�5+      �� -   5         ��Name$ btnClick��5+      $��Valider��6+   $	��100��6=   $��	100,0,0,0q�6I   5,      5-      5.      5/      