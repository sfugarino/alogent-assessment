import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Board } from '../models/board';
import { PostIt } from '../models/post-it';
import { BoardsService } from '../services/boards.service';
import { ToolbarComponent } from './toolbar/toolbar.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  boards: Board[];

  constructor(private dataSrv: BoardsService) { }

  ngOnInit() {
    this.dataSrv.getBoards().subscribe(data => {
      this.boards = data;
    });
  }

  public createBoard(name): void {
    let board = new Board();
    board.name = name;
    this.dataSrv.createBoard(board).subscribe(
      result => {
        this.boards.push(result);
      },
      err => {
        // Log errors if any
        alert("An error occured creating board");
        console.log(err);
      });
  }

  public addPostIt(board, text): void {
    this.dataSrv.addPostIt(board.id, text).subscribe(
      result => {
        board.postIts.push(result);
      },
      err => {
        // Log errors if any
        alert("An error occured adding post");
        console.log(err);
      });
  }

  public deletePostIt(board, postit): void {
    this.dataSrv.deletePostIt(board.id, postit.id).subscribe(
      result => {
        board.postIts.splice(postit, 1);
      },
      err => {
        // Log errors if any
        alert("An error occured deleting post");
        console.log(err);
      });
  }

  public deleteBoard(board: Board): void {
    this.dataSrv.deleteBoard(board.id).subscribe(
      result => {
        let index = this.boards.indexOf(board);
        this.boards.splice(index, 1);
      },
      err => {
        // Log errors if any
        alert("An error occured deleting board");
        console.log(err);
      });
  }
}
