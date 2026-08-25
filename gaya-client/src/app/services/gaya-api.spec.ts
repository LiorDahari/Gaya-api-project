import { TestBed } from '@angular/core/testing';
import { GayaApi } from './gaya-api';

describe('GayaApi', () => {
  let service: GayaApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GayaApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
