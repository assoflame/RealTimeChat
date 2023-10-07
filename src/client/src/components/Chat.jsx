import React, { useEffect, useState } from "react";
import { HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import RoomsList from "./RoomsList";
import Room from "./Room";

const Chat = () => {
    const [connection, setConnection] = useState({});
    const [rooms, setRooms] = useState([]);
    const [currentRoom, setCurrentRoom] = useState('');
    const [roomMessages, setRoomMessages] = useState([]);
    
    useEffect(() => {
        console.log('use effect');
        connect();
    }, []);

    const connect = async () => {
        const connection = new HubConnectionBuilder()
            .withUrl("http://localhost:5285/hubs/chat", {accessTokenFactory: () => 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJOaWNrbmFtZSI6InRlc3QiLCJJZCI6IjY1MjAzYzE3ZDk1N2Q3NmNhOGNkYzU4NSIsImV4cCI6MTY5Njc3NjY2OCwiaXNzIjoidmFsaWRJc3N1ZXIiLCJhdWQiOiJ2YWxpZEF1ZGllbmNlIn0.RVUwFfGFNxMTaZdMa4Kb7iAgzFGj8U4klEFCSjx_fws'})
            .configureLogging(LogLevel.Information)
            .build();

        connection.on("RecieveRooms", (rooms) => {
            setRooms(rooms);
        })

        connection.on("RecieveCurrentRoom", (currentRoom) => {
            setCurrentRoom(currentRoom);
            connection.invoke("GetRoomMessages", currentRoom);
        })

        connection.on("RecieveRoomMessages", (messages) => {
            setRoomMessages(messages);
            console.log(`messages: ${messages.length}`);
        })

        connection.on("RecieveMessage", (message) => {
            roomMessages.push(message);
            setRoomMessages(roomMessages);
        })

        await connection.start();
        await connection.invoke('GetRooms');

        setConnection(connection);
    }
    
    const createRoom = async (roomName) => {
        await connection.invoke("CreateRoom", roomName);
        await connection.invoke("GetRooms");
    }

    const joinRoom = async (roomName) => {
        await connection.invoke("JoinRoom", roomName);
    }

    const sendMessage = async (room, message) => {
        await connection.invoke("SendRoomMessage", room, message);
    }

    return (
        <div>
        <RoomsList rooms={rooms} createRoom={createRoom} joinRoom={joinRoom} key={rooms.length}/>
        {
            currentRoom !== ''
            ? <div><div style={{fontWeight:'bold', fontSize:'27px'}}>{currentRoom}</div><Room room={currentRoom} messages={roomMessages} sendMessage={sendMessage}/></div>
            : <div>Select room</div>
        }
        </div>
    );
}

export default Chat;