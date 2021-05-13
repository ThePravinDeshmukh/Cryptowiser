import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Alert } from 'reactstrap';

interface IProps {
    show: boolean;
    msg: string;
}

interface IMessageState {
    show: boolean;
}

export default class Message extends React.Component<IProps, IMessageState>{
    constructor(props: IProps){
        super(props);
        this.state={
            show:false
        }
    }
    componentDidUpdate(){
        this.setState({show:this.props.show})
      }

    handleClose=()=>{
        this.setState({
            show:false
        })
    }
    render(){
        if(this.props.msg.length<4){
        return (<p></p>);
        }
           

        return(
            <div>
                <Alert variant='danger' show={this.state.show} onClose={this.handleClose} dismissible>
                    Oh snap! You got an error!
                    <p>
                        {this.props.msg}
                    </p>
                </Alert>
            </div>
        )
    }
}