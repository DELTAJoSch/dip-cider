#Getting Started

##Grundlagen

Diese Software funktioniert nur auf Windows-PCs mit installiertem .Net Framework 4.7.2. Wenn Sie Windows 10 mit den neuesten Updates verwenden, sollte das Programm sofort nach der Installation funktionieren. Wenn Sie nicht Windows 10 verwenden, klicken Sie auf [diesen Link](https://dotnet.microsoft.com/download/dotnet-framework/net472) und laden Sie das .Net Framework herunter. Bitte installieren Sie es danach manuell.

##Installation

###Installationsprogramm
Laden Sie das Installationsprogramm herunter und führen Sie es aus.

###Aus dem Quellcode erstellen
Wenn Sie dieses Projekt aus dem Quellcode erstellen möchten, laden Sie den Quellcode als .zip herunter oder klonen Sie das Repository auf Ihren Computer. Nach dem Herunterladen öffnen Sie bitte die .sln-Datei in Visual Studio (2019). Das Projekt sollte nun geladen werden. Stellen Sie die Buildeinstellungen auf "Release" und drücken Sie die Starttaste - danach kopieren Sie den Inhalt des Release-Ordners in einen Ort Ihrer Wahl.

##Einrichtung
Nach erfolgreicher Installation der Anwendung starten Sie bitte die Anwendung. Es wird Ihnen ein Lizenzdialog angezeigt. Akzeptieren Sie die Lizenzen und klicken Sie auf OK. Sie werden nun feststellen, dass die Kartenansichten ausgegraut sind. Bitte besuchen Sie das [Bing Maps Portal](https://www.bingmapsportal.com/) und erstellen Sie ein Konto. Klicken Sie anschließend auf "Meine Schlüssel", um einen neuen Schlüssel zu erstellen. Setzen Sie den Anwendungsnamen auf CIDER und den Key-Type auf Windows App. Kopieren Sie den Schlüssel in eine neue Datei an einem beliebigen Ort. Die Dateiendung dieser Datei sollte .key sein. Fügen Sie den Schlüssel nun zur Anwendung hinzu. Gehen Sie zur Info-Ansicht und drücken Sie "Add new key". Wählen Sie die Datei aus und drücken Sie ok. Sie sollten jetzt in der Lage sein, die Kartenansichten zu nutzen.

##Laden eines Tracks
![Ansicht laden](../images/load.png)

1. Zum Menüpunkt "Load" gehen
2. Drücken Sie "...".
3. Wenn Sie einen Haken sehen, drücken Sie "Load".

Sie haben den Track erfolgreich geladen.