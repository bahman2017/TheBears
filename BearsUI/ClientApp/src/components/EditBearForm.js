import React, {  Component } from 'react'
import axios from 'axios';


class EditBearForm extends Component {
    constructor(props) {
        super(props)
        this.state = { id: props.bear.id, name: props.bear.name, typeName: props.bear.typeName }
    }
   

    EditBear(bear, action) {
        axios.put('/bears', {
            id:bear.id,
            name: bear.name,
            typeName: bear.typeName
        })
            .then(function (response) {

                action();
            })
            .catch(function (error) {
                console.log(error);
            });

    }

    render() {


        const handleInputChange = event => {
            const { name, value } = event.target

            this.setState({ [name]: value })
        }


        return (
            <form
                onSubmit={event => {
                    event.preventDefault()
                    if (!this.state.name || !this.state.typeName||!this.state.id) return

                    this.EditBear(this.state, this.props.action)

                    var bear = { id: null, name: '', typeName: '' }
                    this.setState(bear)
                }}>

                <label>Name</label>
                <input type="text" name="name" value={this.state.name} onChange={handleInputChange} />
                <label>Type</label>
                <input type="text" name="typeName" value={this.state.typeName} onChange={handleInputChange} />
                <button>Edit bear</button>&nbsp;
                <button>Cancel</button>
            </form>
        )
    }
}

export default EditBearForm