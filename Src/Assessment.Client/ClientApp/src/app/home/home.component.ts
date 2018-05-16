import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Board } from '../models/board';
import { PostIt } from '../models/post-it';
import { BoardsService } from '../services/boards.service';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/takeUntil';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  boards: Board[];

  private ngUnsubscribe: Subject<any> = new Subject();

  constructor(private dataSrv: BoardsService) { }

  ngOnInit() {
    this.dataSrv.getBoards().subscribe(data => {
      this.boards = data;
    });
  }

  public createBoard(name): void {
    let board = new Board();
    board.name = name;
    let create = this.dataSrv.createBoard(board).takeUntil(this.ngUnsubscribe).subscribe(
      result => {
        this.boards.push(result);
        create.unsubscribe();
      },
      err => {
        // Log errors if any
        alert("An error occured creating board");
        console.log(err);
      }
    );
  }

  public addPostIt(board, text): void {
    let add = this.dataSrv.addPostIt(board.id, text).takeUntil(this.ngUnsubscribe).subscribe(
      result => {
        board.postIts.push(result);

        add.unsubscribe();
      },
      err => {
        // Log errors if any
        alert("An error occured adding post");
        console.log(err);
      });
  }

  public deletePostIt(board, postit): void {
    this.dataSrv.deletePostIt(board.id, postit.id).takeUntil(this.ngUnsubscribe).subscribe(
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
    this.dataSrv.deleteBoard(board.id).takeUntil(this.ngUnsubscribe).subscribe(
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

  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
