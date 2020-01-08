import React, { Component } from 'react'
import { Spinner, Button } from 'react-bootstrap';
import axios from '../../axios-carpool';

import Rideshares from './Rideshares/Rideshares';
import ModalRideshareForm from './ModalRideshareForm/ModalRideshareForm';
import ModalRideshareDelete from './ModalRideshareDelete/ModalRideshareDelete';

class RideshareManager extends Component {

    defaultRideshareDetail = {
        id: 0,
        startLocation: "",
        endLocation: "",
        startDate: "",
        endDate: "",
        car: {
            id: 0,
            name: "",
            type: "",
            numberOfSeats: 0
        },
        employees: []
    }

    state = {
        rideshares: [],
        allEmployees: [],
        allCars: [],
        loading: true,
        error: false,
        rideshareDetail: {
            ...this.defaultRideshareDetail
        },
        modalIsOpen: false,
        modalAction: "add",
        modalLoading: false,
        modalError: false,
        modalErrorText: "",
        modalDeleteLoading: false,
        modalDeleteOpen: false
    }

    componentDidMount() {
        this.refreshData();
    }

    refreshData = () => {
        axios.get('/rideshares')
            .then(response => {
                this.setState({ rideshares: response.data.rideshares, loading: false });
            })
            .catch(error => {
                this.setState({ error: true, loading: false });
            });
        axios.get('/cars')
            .then(response => {
                this.setState({ allCars: response.data.cars })
            });
        axios.get('/employees')
            .then(response => {
                this.setState({ allEmployees: response.data.employees })
            });
    }

    modalCancelHandler = () => {
        this.setState({
            modalIsOpen: false,
            rideshareDetail: {
                ...this.defaultRideshareDetail
            }
        });
    }

    modalDeleteCancelHandler = () => {
        this.setState({ modalDeleteOpen: false, rideshareId: 0 });
    }

    modalShowHandler = (action, rideshareId) => {
        this.setState({ modalIsOpen: true, modalAction: action, rideshareId: rideshareId, modalLoading: true });
        if (rideshareId) {
            axios.get('rideshares/' + rideshareId)
            .then(response => {
                this.setState({ 
                    rideshareDetail: {
                        id: response.data.id,
                        startLocation: response.data.startLocation,
                        endLocation: response.data.endLocation,
                        startDate: response.data.startDate,
                        endDate: response.data.endDate,
                        car: response.data.car,
                        employees: response.data.employees
                    },
                    modalLoading: false
                 });
            })
            .catch(error => {
                // TODO: Add true error handling. Get error message and display on form
                this.setState({ modalError: true, modalLoading: false });
            });
        }
        this.setState({ modalLoading: false });
    }

    modalShowDeleteHandler = (rideshareId) => {
        this.setState({ modalDeleteOpen: true, rideshareId: rideshareId });
    }

    onChangeStartDate = (startDate) => {
        this.setState(prevState => (
            { rideshareDetail: {
                ...prevState.rideshareDetail,
                startDate: startDate
            }}
        ));
    }

    onChangeEndDate = (endDate) => {
        this.setState(prevState => (
            { rideshareDetail: {
                ...prevState.rideshareDetail,
                endDate: endDate
            }}
        ));
    }

    onChangeCar = (car) => {
        this.setState(prevState => (
            { rideshareDetail: {
                ...prevState.rideshareDetail,
                car: {
                    ...prevState.allCars.find(c => (
                        c.id === car.value
                    ))
                }
            }}
        ));
    }

    onChangeEmployees = (employeeIds) => {
        let employees = [];
        if(employeeIds && employeeIds.length) {
            employees = employeeIds.map(item => {
                let employee = this.state.allEmployees.find(e => (
                    e.id === item.value
                ));
                return employee;
            });
        }

        this.setState(prevState => {
            return (
                { rideshareDetail:{
                    ...prevState.rideshareDetail,
                    employees: employees
                } }
            );
        });
    }

    onChangeField = (e) => {
        const name = e.target.name;
        const value = e.target.value;
        this.setState(prevState => (
            { rideshareDetail: {
                ...prevState.rideshareDetail,
                [name]: value
            } }
        ));
    }

    onSubmitAdd = (e) => {
        // Call POST /rideshares
        let body = {
            ...this.state.rideshareDetail
        };
        this.setState({ modalUpdateLoading: true })

        axios.post('/rideshares', body)
            .then(response => {
                this.setState({ modalUpdateLoading: false });
                this.modalCancelHandler();
                this.refreshData();
            })
            .catch(error => {
                this.setState({ modalUpdateLoading: false, modalError:true, modalErrorText: error.data })
            })

        e.preventDefault();
    }

    onSubmitEdit = (e) => {
        // Call PUT /rideshares/{id}
        let body = {
            ...this.state.rideshareDetail
        };
        this.setState({ modalUpdateLoading: true });

        axios.put('/rideshares/' + body.id, body)
            .then(repsonse => {
                this.setState({ modalUpdateLoading: false });
                this.modalCancelHandler();
                this.refreshData();
            })
            .catch(error => {
                this.setState({ modalUpdateLoading: false, modalError: true, modalErrorText: error.data })
            })

        e.preventDefault();
    }

    onSubmitDelete = (e) => {
        this.setState({ modalDeleteLoading: true });

        axios.delete('rideshares/' + this.state.rideshareId)
            .then(response => {
                this.setState({ modalDeleteLoading: false });
                this.modalDeleteCancelHandler();
                this.refreshData();
            })
            .catch(error => {
                this.setState({ modalDeleteLoading: false, modalDeleteError: true, modalDeleteErrorText: error.data})
            })

        e.preventDefault();
    }

    render () {
        let rideshares = this.state.error 
            ? <p>Rideshares can't be loaded!</p> 
            : <Spinner animation="border" />

        if (!this.state.loading) {
            rideshares = (
                <Rideshares
                     rideshares={this.state.rideshares}
                     modalShowHandler={this.modalShowHandler}
                     modalShowDeleteHandler={this.modalShowDeleteHandler} />
            )
        }
        
        return (
            <>
                <ModalRideshareForm 
                    show={this.state.modalIsOpen} 
                    handleClose={this.modalCancelHandler} 
                    action={this.state.modalAction}
                    rideshareDetail={this.state.rideshareDetail}
                    error={this.state.modalError}
                    errorText={this.state.modalErrorText}
                    loading={this.state.modalLoading}
                    onSubmitEdit={this.onSubmitEdit}
                    onSubmitAdd={this.onSubmitAdd}
                    onChangeField={this.onChangeField}
                    onChangeStartDate={this.onChangeStartDate}
                    onChangeEndDate={this.onChangeEndDate}
                    onChangeCar={this.onChangeCar}
                    onChangeEmployees={this.onChangeEmployees}
                    allCars={this.state.allCars}
                    allEmployees={this.state.allEmployees} >
                </ModalRideshareForm>
                <ModalRideshareDelete 
                    show={this.state.modalDeleteOpen}
                    handleClose={this.modalDeleteCancelHandler}
                    onSubmitDelete={this.onSubmitDelete}
                    loading={this.modalDeleteLoading}>
                </ModalRideshareDelete>
                <h1>Ride sharing management</h1>
                <Button onClick={() => this.modalShowHandler("add", null)}>Add Item</Button>
                {rideshares}
            </>
        );
    }
}

export default RideshareManager;