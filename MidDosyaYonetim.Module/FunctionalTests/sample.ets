;#DropDB MidDosyaYonetimEasyTest

#Application MidDosyaYonetim.Win
#Application MidDosyaYonetim.Web

*FillForm
 User Name = Admin
*Action Log In

#IfDef MidDosyaYonetim.Win

*Action About...
 
*Action OK

#EndIf

#IfDef MidDosyaYonetim.Web
*Action Navigation(My Details)
#EndIf
