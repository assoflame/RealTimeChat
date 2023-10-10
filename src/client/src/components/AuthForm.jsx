import React, { useState } from "react";
// import { Form, Button } from "react-bootstrap";
import '../styles/App.css'


const AuthForm = ({action, actionName}) => {
    const [nickname, setNickname] = useState('');
    const [password, setPassword] = useState('');

    return (
        // <Form className="authForm"
        //     onSubmit = {e => {
        //         e.preventDefault();
        //         action(nickname, password);
        //     }}>
        //     <Form.Group>
        //         <Form.Control className='authInput' placeholder='nickname' onChange={e => setNickname(e.target.value)}/>
        //         <Form.Control className='authInput' placeholder='password' type="password" onChange={e => setPassword(e.target.value)}/>
        //     </Form.Group>

        //     <Button variant="success" type="submit" disabled={!nickname || !password}>{actionName}</Button>
        // </Form>

        <form className="authForm"
            onSubmit = {e => {
                e.preventDefault();
                action(nickname, password);
            }}>
                <div className="authInputs">
                    <input className="authInput" placeholder='nickname' onChange={e => setNickname(e.target.value)}/>
                    <input className="authInput" placeholder='password' type="password" onChange={e => setPassword(e.target.value)}/>
                </div>
            <button className="authButton" type="submit" disabled={!nickname || !password}>{actionName}</button>
        </form>
    )
}

export default AuthForm;