   M S B A M L   `   `   `  ����H  DRejestrFaktur, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:RejestrFaktur.ViewsRejestrFaktur.Views  5   	   C&clr-namespace:RejestrFaktur.ConvertersRejestrFaktur.Converters  5   	   E'clr-namespace:RejestrFaktur.ValidationsRejestrFaktur.Validations  5   	   .    (RejestrFaktur.Views.AddItemToInvoiceView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:RejestrFaktur.Views     5   	   4c&clr-namespace:RejestrFaktur.Converters     5   	   5v'clr-namespace:RejestrFaktur.Validations     5   	     � Title$#  Dodawanie Elementu Do Faktury��5
   	   $	��530��6/   $	��470��6<    � 	Resources 5      @   :RejestrFaktur.Converters.StringToFloatingPercentsConverter   TaxSliderConverter  validationTemplate��%�   &	        &	        5   
   6.   �� 5   
   .R�5      �� .+�5      �� �� 5      
���  ��$��10(�6-   ��6;   �� *ErrorContent+5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��50 �6   �� 5      $��30 �6   �� 5      $��50 �6   �� 5      $��30 �6   �� 5      $��50 �6   �� 5      $��30 �6   �� 5      $��50 �6   �� 5      $��30 �6   �� 5      $��50 �6   �� 5       $��30 �6   �� 5!      $��50 �6   �� 5"      $��30 �6    � ColumnDefinitions 5$   
   �� 5%      $��30 �6   �� 5&      $	��170 �6   �� 5'      $��50 �6   �� 5(      $	��190 �6   �� 5)      $��30 �6   .+�5+   
   �� $��Nazwa:��6   $��1��6!   $��1��61   $��Center=�6>   �� 5,   
   $��3��6   $��1��6"   $��Center=�6/   $��30��6J   $��Center=�6�   ��6V   �� *
ItemName+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5-   
   $��Miara:��6   $��1��6!   $��3��61   $��Center=�6>   �� 5.   
   $��3��6   $��3��6#   $��30��60   $��Center=�6^   ��6<   �� *	ItemUnits+ �� SelectedIndex 6�   �� *
ItemUnit+$ PropertyChanged=��� 5/   
   $��Ilość:��6   $��1��6!   $��5��61   $��Center=�6>   �� 50   
   -   5         ��Name$ 
itemAmount��50   �   $��3��6   $��5��6"   $��Center=�6/   $��30��6J   $��Center=�6V    =� ErrorTemplate# [ 6x   .��6�   ��51      �� 52       �� Path$ 
ItemAmount�6   $ PropertyChanged=�6,   	 �� Mode$	 TwoWay=�6R   
 �� ValidationRules
 53      8   2RejestrFaktur.Validations.ItemAmountValidationRule  54      56      .��57      58      �� 59   
   $��2��6   $&�� Cena netto (za jeden przedmiot):��6$   $��1��6O   $��7��6_   $��Center=�6l   �� 5:   
   -   5        $ netPrice��5:   x   $��3��6   $��7��6"   $��Center=�6/   $��30��6J   $��Center=�6V   # [ 6�   .��6�   ��5;      �� 5<      $ ItemNetPrice�6   $ PropertyChanged=�6.   $	 TwoWay=�6T   
 5=      6   0RejestrFaktur.Validations.NetPriceValidationRule  5>      5@      .��5A      5B      �� 5C   
   $��Stawka podatku:��6   $��1��6*   $��9��6:   $��Center=�6G   �� 5D   
   $��2��6   $��9��6    $��Center=�6-   $
��Left=�6H   ��6c   ��  �� ElementName$ taxRateSlider��$ Value� �� 	Converter# [  �� 5E   
    �� Orientation$ 
Horizontal=�6   $��3��6.   $��9��6>   .+�5F      �� $��-��6   $��20��6!   $��20��6,    � Cursor$
 Hand��6^   ��68   �� *SliderMinusButton+6^   �� 5G      -   5        $��taxRateSlider��5G   0   $��Center=�6   $	��145��6E   $��30��6Q   $��5 10 0 0q�6]    � Minimum$ 0Q�6o    � Maximum$
 1000Q�6{    �� TickFrequency$ 1Q�6�    �� IsSnapToTickEnabled$
 True��6�    � Value 6�   �� *ItemTaxRate+$ PropertyChanged=��� 5H      $��+��6   $��20��6!   $��20��6,   $
 Hand��6]   ��68   �� *SliderPlusButton+6]   5I      �� 5J   
   $��DODAJ KOLEJNY ELEMENT��6   $��1��61   $��11��6A   $
 Hand��6x   ��6O   �� *AddAnotherItemButton+��5K      �� 5L       �� 
TargetType" ��6   .�5M      ��  �� Property � {� �� Value$ False��62    �� Triggers 5N      ^� 5O       ^� 
Conditions 5P      �� 5Q   "    �� Binding 6,   �� $ (Validation.HasError)�$ 
itemAmount�� �� Value	 False6s   .0�5S      ��  � {�$
 True��6:   5T      �� 5U       �� Binding 6&   �� *
ItemName+ �� Value 6C   Z� .a�5V      ��  � {�$ False��6:   5W      �� 5X       6&   �� *
ItemAmount+ 6E   Z� .a�5Y      ��  � {�$ False��6:   5Z      �� 5[       6&   �� *
ItemUnit+ 6C   Z� .a�5\      ��  � {�$ False��6:   5]      �� 5^       6&   �� *ItemNetPrice+ 6G   Z� .a�5_      ��  � {�$ False��6:   5`      �� 5a       6&   �� *ItemTaxRate+ 6F   Z� .a�5b      ��  � {�$ False��6:   5c      5e      5g      �� 5h   
   $��ZAPISZ I ZAKOŃCZ��6   $��3��6,   $��11��6<   $
 Hand��6o   ��6J   �� *AddAndSaveButton+��5i      �� 5j      " ��6   .�5k      ��  � {�$ False��62    5l      ^� 5m       5n      �� 5o   "    6,   �� $ (Validation.HasError)�$ 
itemAmount��	 False6s   �� 5p   "    6,   �� $ (Validation.HasError)�$ netPrice��	 False6q   .0�5r      ��  � {�$
 True��6:   5s      �� 5t       6&   �� *
ItemName+ 6C   Z� .a�5u      ��  � {�$ False��6:   5v      �� 5w       6&   �� *
ItemAmount+ 6E   Z� .a�5x      ��  � {�$ False��6:   5y      �� 5z       6&   �� *
ItemUnit+ 6C   Z� .a�5{      ��  � {�$ False��6:   5|      �� 5}       6&   �� *ItemNetPrice+ 6G   Z� .a�5~      ��  � {�$ False��6:   5      �� 5�       6&   �� *ItemTaxRate+ 6F   Z� .a�5�      ��  � {�$ False��6:   5�      5�      5�      5�      5�      