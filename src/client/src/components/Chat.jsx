import React, { useEffect, useState } from "react";
import { HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import RoomsPanel from "./RoomsPanel";
import Room from "./Room";
import { getAccesToken} from "../helpers/Auth";
import '../styles/Chat.css'

const Chat = () => {
    const [connection, setConnection] = useState({});
    const [rooms, setRooms] = useState([]);
    const [currentRoom, setCurrentRoom] = useState('');
    const [roomMessages, setRoomMessages] = useState([]);
    const [roomUsers, setRoomUsers] = useState([]);
    const [adminRights, setAdminRights] = useState(false);
    
    useEffect(() => {
        console.log('use effect');
        connect();
    }, []);

    const connect = async () => {
        const connection = new HubConnectionBuilder()
            .withUrl("http://localhost:5285/hubs/chat", {accessTokenFactory: () => getAccesToken()})
            .configureLogging(LogLevel.Information)
            .build();

        connection.on("RecieveRooms", (rooms) => {
            setRooms(rooms);
        })

        connection.on("RecieveCurrentRoom", (roomInfo) => {
            setCurrentRoom(roomInfo.room);
            setAdminRights(roomInfo.adminRights);
            console.log(roomInfo.adminRights);
            connection.invoke("GetRoomMessages", roomInfo.room);
        })

        connection.on("RecieveRoomMessages", (messages) => {
            setRoomMessages(messages);
        })

        connection.on("RecieveMessage", (message) => {
            setRoomMessages(roomMessages => [...roomMessages, message]);
        })

        connection.on("RecieveRoomUsers", (users) => {
            setRoomUsers([...users]);
        })

        connection.on("RecieveCreateRoomResult", (result) => {
            if(result === false)
                alert('This room already exists');
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
        if(currentRoom !== '')
            await connection.invoke("LeaveRoom", currentRoom);
        await connection.invoke("JoinRoom", roomName);
    }

    const sendMessage = async (room, message) => {
        await connection.invoke("SendRoomMessage", room, message);
    }

    const blockUser = async (username) => {
        await connection.invoke("BlockRoomUser", currentRoom, username);
    }

    return (
        <div className='chat'>
            <RoomsPanel rooms={rooms} createRoom={createRoom} joinRoom={joinRoom} key={rooms.length}/>
            {
                currentRoom !== ''
                && <Room id='room' room={currentRoom} messages={roomMessages}
                                    sendMessage={sendMessage} roomUsers={roomUsers}
                                    adminRights={adminRights} blockUser={blockUser}/>
            }
        </div>
    );
}

export default Chat;