import { useState, useEffect } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import * as signalR from "@microsoft/signalr";

function App() {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null)
  const [connectionStatus, setConnectionStatus] = useState("Disconnected")
  const [errorMessage, setErrorMessage] = useState("")
  const [message, setMessage] = useState("")

  useEffect(() => {
    // Create the connection
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("/hub", {
        skipNegotiation: false,
        transport: signalR.HttpTransportType.WebSockets
      })
      .withAutomaticReconnect([0, 2000, 10000, 30000]) // Retry with backoff
      .configureLogging(signalR.LogLevel.Information)
      .build();

    // Set up the message handler
    connection.on("ReceiveMessage", (message: string) => {
      const m = document.getElementById("messages");
      if (m) {
        m.innerHTML += `<div>${message}</div>`;
      }
    });

    // Start the connection with better error handling
    const startConnection = async () => {
        setConnectionStatus("Connecting...");
        await connection.start();
        setConnectionStatus("Connected");
        console.log("SignalR Connected successfully");
    };

    // Start the connection
    startConnection();

    setConnection(connection);

    // Clean up on component unmount
    return () => {
      connection.stop();
    };
  }, []);

  const sendMessage = async () => {
    if (connection && message.trim()) {
      try {
        // Wywołaj metodę SendMessage na hubie
        await connection.invoke("SendMessage", message);
        console.log("Message sent successfully");
        setMessage(""); // Wyczyść pole po wysłaniu
      } catch (err) {
        console.error("Error sending message: ", err);
      }
    }
  };

  return (
    <>
      <div>
        <input 
          type="text" 
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          onKeyPress={(e) => e.key === 'Enter' && sendMessage()}
        />
        <button onClick={sendMessage}>Send</button>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
        <div>SignalR Status: {connectionStatus}</div>
        {errorMessage && <div className="error">Error: {errorMessage}</div>}
        <div id="messages"></div>
      </div>
      <h1>Vite + React</h1>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </>
  )
}

export default App
