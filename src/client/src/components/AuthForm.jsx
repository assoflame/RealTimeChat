import React, { useState } from "react";
import { Form, Button } from "react-bootstrap";


const AuthForm = ({action, actionName}) => {
    const [nickname, setNickname] = useState('');
    const [password, setPassword] = useState('');

    return (
        <Form className="authForm"
            onSubmit = {e => {
                e.preventDefault();
                action(nickname, password);
            }}>
            <Form.Group>
                <Form.Control className='authInput' placeholder='nickname' onChange={e => setNickname(e.target.value)}/>
                <Form.Control className='authInput' placeholder='password' type="password" onChange={e => setPassword(e.target.value)}/>
            </Form.Group>

            <Button variant="success" type="submit" disabled={!nickname || !password}>{actionName}</Button>
        </Form>
    )
}

export default AuthForm;