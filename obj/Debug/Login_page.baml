   M S B A M L   `   `   `  ����H  DQuestionnaire, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:QuestionnaireQuestionnaire  5   	       Questionnaire.Login_page   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Questionnaire     5   	   -   5          � Title$$  Вход в программу��5   	   $	��300��6"   $	��450��6/    � 	Resources 5
         	alignment �� Property �� Value �� 
TargetType �� BasedOn��%&  &	        '�� j     7   �� 5   
   .�5      ��  � ��$ Center=�6<   �� 5       � ��$ Center=�6:   5      �� 5   
   " ��6   8   .�5      ��  � ��$ 18(�61   �� 5       � ��$ Times New Roman)�63   �� 5       � ��$
 Bold"�63   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��0.7* �6   �� 5      $
��0.9* �6    � RowDefinitions 5   
   �� 5      $
��0.7* �6   �� 5      $
��0.7* �6   �� 5       $
��0.7* �6   �� 5!      $
��0.4* �6   .+�5&   
   �� -   5         ��Name$ _label��5&      $��Введите ФИО��6!   �� 5'   
   -   5        $ _label1��5'      $��1��6   $!��Выберите школу��6.   �� 5(   
   -   5        $ _label2��5(      $��2��6   $!��Выберите класс��6.   �� 5*   
   -   5        	 ��Name$	 _placehoder��5*   $   $��1��6   $
��16px(�69   ���#8998A5$��Italic$�6^   #��[  6q   $"Введите полное ФИО6�   �� 5+   
   -   5        
 ��Name$
 _input��5+   "   $��1��6   #��[  62    �� TextWrapping$
 Wrap=�6U   $��0q�6i   
������ $
��16px(�6�   $	��165��6�   $	��165��6�   $��Regular"�6�   $��Times New Roman)�6�   5,   ?   �� 5.   
   -   5         ��Name$ 
_schoolBox��5.   0   $��1��6   $��1��6    $	��120��6F    �� DisplayMemberPath$ School_Number��6R   #��[  6w   6�   �� 5/   
   -   5        $ 	_classBox��5/   0   $��2��6   $��1��6    $	��120��6D   $ 
Klass_Name��6P   #��[  6q   �� 51   
   -	   5         ��Name$ _entrenceButt��51   .   $��2��6   $��3��6!   $��Продолжить��6E   $��75��6Z   #��[  6e   6�   52      53      