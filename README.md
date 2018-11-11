# ez.nlog.telegram

Ez.NLog.Telegram
==========
An NLog target for Telegram

Usage
=====

### NLog.config

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <extensions>
    <add assembly="Ez.NLog.Telegram" />
  </extensions>

  <targets>
    <target xsi:type="Telegram"
            name="Telegram"
			      layout="${level:uppercase=true}|${message} ${exception}"
            botToken ="xxx"
            chatId="xxx" />
  </targets>

  <rules>
    <logger name="*" minlevel="Debug" writeTo="Telegram" />
  </rules>
</nlog>
```
