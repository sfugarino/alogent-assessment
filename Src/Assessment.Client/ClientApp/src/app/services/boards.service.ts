import { Injectable } from '@angular/core';
import { Board } from '../models/board';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PostIt } from '../models/post-it';
import { ContentChild } from '@angular/core/src/metadata/di';

@Injectable()
export class BoardsService {

  constructor(private http: HttpClient) { }

  getBoards(): Observable<Board[]> {
    return this.http.get<Board[]>('/api/boards');
  }

  createBoard(board: Board): Observable<Board> {
    let url = '/api/boards';
    const headers = new HttpHeaders(
      {
        'Content-Type': 'application/json'
      });
    return this.http.post<Board>(url, board, { headers: headers });
  }

  deleteBoard(id: number): Observable<Board> {
    let url =  '/api/boards/' + id.toString();

    return this.http.delete<Board>(url);
  }

  addPostIt(boardId: number, text: string): Observable<PostIt> {
    let url = '/api/boards/postit/' + boardId + '/' + encodeURIComponent(text);
    return this.http.post<PostIt>(url, null);
  }

  deletePostIt(boardId: number, postitId: number): Observable<PostIt> {
    let url = '/api/boards/postit/' + boardId + '/' + postitId;
    return this.http.delete<PostIt>(url);
  }
}
