   M S B A M L   `   `   `  ����>  :Tp3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Tp3Tp3  5   	       Tp3.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:Tp3     5   	   -   5          � WindowStyle$  
ToolWindow=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � Title$ Jeu Adriana et Christian��5   	   $
��1050��6*   $	��900��68   .��5      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      $	��200 �6   6-   .+�5   
   � $��0��6   .+�5      �� -   5        $��NiveauBackground��5   Y   $*��$images/Niveaux/BackgroundNiveau1.png��6   $��0,-1214,0,-18q�6B   6t   �� 5      -   5         ��Name$ Surface��5      $��0��6&   5      5      � 5   
   $��1��6   ��5      �� 5       �� ImageSource$" images/HUD/HUDBackground.png��6   6J   .+�5!      �� $��1��6   
���   �$��2q�69   6O   �� 5-      $��Bottom=�6   $��	0,0,0,100q�64   .+�5.      �� -   5         ��Name$ 	ViePirate��5.      
�������$��Verdana)�6H   $��15(�6]   $
��Bold"�6k   $��	40,0,0,20q�6}   6�   �� 5/      -   5        $ 
VieEnnemie��5/      
�������$��Verdana)�6I   $��15(�6^   $
��Bold"�6l   $��	40,0,0,20q�6~   6�   50      �� 52      $��Bottom=�6   $��0,0,0,30q�64   .+�54      �� -   5        .��54   5   Tuer enemis6B   �� 56      -   5        $��TextBlockNiveauActuel��56   q   
�������$��30(�64   $��Center=�6B   $��0,0,0,20q�6_   $��	Niveau: ?��6�   6�   �� 57      -   5        $	��130��57      $��50��6%   $��25(�61   |�.  .��6o   	Magasin6x   58      5:      5<      5?      