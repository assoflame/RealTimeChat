import React, { useEffect, useRef, useState } from "react";
import RoomUsersList from "./RoomUsersList";
import '../styles/Room.css'
import Message from "./Message";

const Room = ({room, messages, sendMessage, roomUsers, adminRights, blockUser}) => {
    const [message, setMessage] = useState('');
    const messageRef = useRef();

    useEffect(() => {
        if(messageRef && messageRef.current) {
            const {scrollHeight, clientHeight} = messageRef.current;
            messageRef.current.scrollTo({
                left: 0, top: scrollHeight - clientHeight,
                behavior: 'smooth'
            });
        }
    })

    return (
        <div id="room">
            <div id="roomMain">
                <div id="currentRoomName">{room}</div>

                <div ref={messageRef} id="messages">
                    {messages.map((message, index) => <Message key={index} message={message}/>)}
                </div>

                <div id="writeMessagePanel">
                    <textarea id="messageInput" placeholder="Write message..." value={message} type="text" onChange={e => setMessage(e.target.value)}/>
                    <button id="sendMessageButton" onClick={e => {sendMessage(room, message); setMessage('')}}>Send</button>
                </div>
            </div>

            <RoomUsersList roomUsers={roomUsers} blockUser={blockUser} adminRights={adminRights}/>
        </div>
    );
}

export default Room;