import React from 'react'
import { Modal, Button, Form, Spinner } from 'react-bootstrap';

const modalRideshareDelete = (props) => (
    <Modal show={props.show} onHide={props.handleClose} >
        <Modal.Header closeButton >
            <Modal.Title>Delete rideshare</Modal.Title>
        </Modal.Header>
        <Form onSubmit={props.onSubmitDelete}>
            <Modal.Body>
                Are you sure you want to delete this rideshare?
                {props.loading ? <Spinner animation="border" /> : null}
            </Modal.Body>
            <Modal.Footer>
                {/* <input type="submit" value="Delete" color="primary" className="btn btn-primary" /> */}
                <Button type="submit" variant="danger" >Delete</Button>
                <Button variant="primary" onClick={props.handleClose}>Cancel</Button>
            </Modal.Footer>
        </Form>
    </Modal>
);

export default modalRideshareDelete;